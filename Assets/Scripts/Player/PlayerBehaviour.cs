using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private PlayerCharacteristic characteristic = null;

    [Header("Player")]
    [SerializeField] private Transform player;
    private bool _isAlive = true;
    private Quaternion _shipDefaultRotation;

    [Header("Bullet")]
    [SerializeField] private GameObject bullet = null;
    [SerializeField] private Transform bulletSpawnPoint = null;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI coordinates = null;
    [SerializeField] private TextMeshProUGUI playerSpeed = null;
    [SerializeField] private TextMeshProUGUI angle = null;
    [SerializeField] private TextMeshProUGUI laserColdown = null;
    [SerializeField] private TextMeshProUGUI laserCharges = null;

    [Header("Laser")]
    [SerializeField] private Transform laserSpawnPoint;
    [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField] private LineRenderer laserLine;

    private PlayerInputActions playerInputActios = null;
    private UpdatePlayerStatistic playerStats = null;
    private Shooting playerShooting = null;
    private PlayerMovement movement = null;
    private void Awake()
    {
        playerInputActios = new PlayerInputActions();
        playerStats = new UpdatePlayerStatistic();
        playerShooting = new Shooting();
        movement = new PlayerMovement();
    }
    private void Start()
    {
        _shipDefaultRotation = player.localRotation;
        playerShooting.SetupLaserCharges(characteristic.MaxmumLaserShots, characteristic.CurrentLasetShots);
    }
    private void OnEnable()
    {
        playerInputActios.Enable();

        playerInputActios.Player.MoveForward.started += StartMovement;
        playerInputActios.Player.MoveForward.canceled += StopMovement;
        playerInputActios.Player.Rotate.started += StartRotation;
        playerInputActios.Player.Rotate.canceled += StopRotation;

        playerInputActios.Player.ShootBullet.performed += ShootBullet;
        playerInputActios.Player.ShootLaser.performed += ShootLaser;
    }

    private void Update()
    {
        playerShooting.CheckLaserCharges(this, characteristic.LaserReloadTime);
        if (_isAlive)
        {
            playerStats.ShowAngle(angle, GameManager.instance.player.transform.rotation.z * 180f);
            playerStats.ShowPosition(coordinates, transform);
            playerStats.ShowSpeed(playerSpeed, transform);
            playerStats.ShowLaserCharges(laserCharges, playerShooting.LaserCharges);
            playerStats.ShowLaserColdown(laserColdown, playerShooting.LaserColdownTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid" || collision.gameObject.tag == "Enemy")
        {
            GameManager.instance.onGameEnd.Invoke();
        }
    }
    
    private void ShootBullet(InputAction.CallbackContext inputAction)
    {
        playerShooting.ShootBullet(bullet, bulletSpawnPoint, 0.1f);
    }

    private void ShootLaser(InputAction.CallbackContext inputAction)
    {
        playerShooting.ShootLaser(transform, this, laserLine, whatIsEnemy, characteristic.LaserDistance, characteristic.LaserDelay, characteristic.LaserShotColdown);
    }

    private void OnDisable()
    {
        playerInputActios.Player.MoveForward.started -= StartMovement;
        playerInputActios.Player.MoveForward.canceled -= StopMovement;
        playerInputActios.Player.Rotate.started -= StartRotation;
        playerInputActios.Player.Rotate.started -= StopRotation;


        playerInputActios.Player.ShootBullet.performed -= ShootBullet;
        playerInputActios.Player.ShootLaser.performed -= ShootLaser;

        playerInputActios.Disable();
    }

    private void StartMovement(InputAction.CallbackContext obj)
    {
        movement.StartMovement(this, characteristic.MovementSpeed);
    }

    private void StopMovement(InputAction.CallbackContext obj)
    {
        movement.StopMovement(this);
    }

    private void StartRotation(InputAction.CallbackContext obj)
    {
        movement.StartRotation(this, characteristic.RotationSpeed, obj.ReadValue<float>());
    }

    private void StopRotation(InputAction.CallbackContext obj)
    {
        movement.StopRotation(this, obj.ReadValue<float>());
    }
}
