using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting 
{
    private Coroutine showLaser = null, restoreLaserCharge = null;

    private float nextShotTime = 0.0f, nextLaserShotTime = 0.0f;

    private float restoreSeconds;
    private int laserCharges, maximumCharges;

    private const float ONE_SECOND = 1.0f, ZERO_SECONDS = 0.0f;
    private const float STOPPED_TIME = 0.0f;
    private const int NONE_OF_CHARGES = 0;

    public int LaserCharges
    {
        get => laserCharges;
        set
        {
            laserCharges = value;

            if (laserCharges > maximumCharges)
                laserCharges = maximumCharges;
        }
    }

    public float LaserColdownTime
    {
        get => restoreSeconds;
    }

    public void SetupLaserCharges(int maxCharges, int currentCharges)
    {
        maximumCharges = maxCharges;
        LaserCharges = currentCharges;
    }

    public void CheckLaserCharges(MonoBehaviour monoBehaviour, float restoreTime)
    {
        if (laserCharges < maximumCharges && restoreLaserCharge == null)
        {
            restoreLaserCharge = monoBehaviour.StartCoroutine(RestoreLaserCharge(restoreTime));
        }
    }

    public void ShootLaser(Transform origin, MonoBehaviour monoBehaviour, LineRenderer line, LayerMask enemy, float laserDistance, float showLaserDelay, float shootingDelay)
    {
        if (Time.time > nextLaserShotTime && LaserCharges > NONE_OF_CHARGES && Time.timeScale != STOPPED_TIME)
        {
            int positionCount = 2, firstIndex = 0, secondIndex = 1;
            float firstIndexZOffset = -0.01f;

            RaycastHit2D[] raycastHits = Physics2D.RaycastAll(origin.position, origin.up, laserDistance, enemy.value);

            if (showLaser != null)
            {
                monoBehaviour.StopCoroutine(showLaser);
            }
            showLaser = monoBehaviour.StartCoroutine(ShowAndDisableLaser(line, showLaserDelay));

            line.positionCount = positionCount;
            line.SetPosition(firstIndex, new Vector3(origin.position.x, origin.position.y, firstIndexZOffset));
            line.SetPosition(secondIndex, origin.up * laserDistance);

            foreach (RaycastHit2D hit in raycastHits)
            {
                Object.Destroy(hit.collider.gameObject);
            }

            nextLaserShotTime = Time.time + shootingDelay;
            LaserCharges--;
        }
    }

    public void ShootBullet(GameObject bullet, Transform spawnPosition, float shootingDelay)
    {
        if (Time.time > nextShotTime && Time.timeScale != STOPPED_TIME)
        {
            Object.Instantiate(bullet, spawnPosition.position, spawnPosition.rotation);
            nextShotTime = Time.time + shootingDelay;
        }

    }

    private IEnumerator ShowAndDisableLaser(LineRenderer line, float showLaserDelay)
    {
        line.enabled = true;
        yield return new WaitForSeconds(showLaserDelay);
        line.enabled = false;
    }

    private IEnumerator RestoreLaserCharge(float restoreTime)
    {
        restoreSeconds = restoreTime;

        while (restoreSeconds >= ZERO_SECONDS)
        {
            restoreSeconds -= ONE_SECOND * Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        LaserCharges++;
        restoreLaserCharge = null;
    }

}
