using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

class PlayerMovement
{
    private float moveSpeed = 0.0f;
    private float rotSpeed = 0.0f;

    private const float STOPPING_SPEED = 0.0f;

    private Coroutine movement = null, stopMotion = null, rotation = null, stopRotation = null;

    public void StartMovement(MonoBehaviour monoBehaviour, float movementSpeed)
    {
        if (stopMotion != null)
        {
            monoBehaviour.StopCoroutine(stopMotion);
        }

        if (movement != null)
        {
            monoBehaviour.StopCoroutine(movement);
        }
        movement = monoBehaviour.StartCoroutine(StartMotion(monoBehaviour.transform, movementSpeed));
    }

    public void StopMovement(MonoBehaviour monoBehaviour)
    {
        monoBehaviour.StopCoroutine(movement);
        stopMotion = monoBehaviour.StartCoroutine(StopMotion(monoBehaviour.transform));
    }

    public void StartRotation(MonoBehaviour monoBehaviour, float rotationSpeed, float direction)
    {
        if (stopRotation != null)
        {
            monoBehaviour.StopCoroutine(stopRotation);
        }

        if (rotation != null)
        {
            monoBehaviour.StopCoroutine(rotation);
        }
        rotation = monoBehaviour.StartCoroutine(StartRotationCoroutine(monoBehaviour.transform, rotationSpeed, direction));
    }

    public void StopRotation(MonoBehaviour monoBehaviour, float direction)
    {
        monoBehaviour.StopCoroutine(rotation);
        stopRotation = monoBehaviour.StartCoroutine(StopRotationCoroutine(monoBehaviour.transform, direction));
    }

    public IEnumerator StartRotationCoroutine(Transform player, float rotationSpeed, float direction)
    {
        while (rotSpeed < rotationSpeed)
        {
            rotSpeed += 1.0f * Time.deltaTime;
            player.Rotate(Vector3.forward * rotSpeed * direction);
            yield return new WaitForEndOfFrame();
        }

        while (true)
        {
            player.Rotate(Vector3.forward * rotSpeed * direction);
            yield return new WaitForEndOfFrame();
        }

    }

    public IEnumerator StopRotationCoroutine(Transform player, float direction)
    {
        rotSpeed = 0;
        yield return null;
    }

    private IEnumerator StartMotion(Transform player, float movementSpeed)
    {
        while (moveSpeed < movementSpeed)
        {
            moveSpeed += 1.0f * Time.deltaTime;

            player.Translate(moveSpeed * Time.deltaTime * Vector2.up);

            yield return new WaitForEndOfFrame();
        }

        while (true)
        {
            player.Translate(movementSpeed * Time.deltaTime * Vector2.up);

            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator StopMotion(Transform player)
    {
        while (moveSpeed > STOPPING_SPEED)
        {
            moveSpeed -= 1.0f * Time.deltaTime;

            player.Translate(moveSpeed * Time.deltaTime * Vector2.up);

            yield return new WaitForEndOfFrame();
        }
    }

}