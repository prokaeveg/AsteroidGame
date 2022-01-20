using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCharacteristic", menuName = "ScriptableObjects/PlayerCharacteristic", order = 1)]
public class PlayerCharacteristic : ScriptableObject
{
    [SerializeField, Min(0.0f)] private float moveSpeed = 2.0f;
    [SerializeField, Min(0.0f)] private float rotSpeed = 20.0f;
    [SerializeField, Min(0.0f)] private float laserRealoadTime = 10.0f;

    [SerializeField, Min(0.0f)] private float laserShotColdown = 0.1f;
    [SerializeField, Min(0.0f)] private float laserDistance = 100.0f;
    [SerializeField, Min(0.0f)] private float laserDelay = 0.2f;
    [SerializeField, Min(0)] private int maxLasetShots = 2;
    [SerializeField, Min(0)] private int currLaserShots = 2;

    public int CurrentLasetShots => currLaserShots;
    public float MovementSpeed => moveSpeed;
    public float RotationSpeed => rotSpeed;
    public float LaserReloadTime => laserRealoadTime;
    public float LaserDistance => laserDistance;
    public int MaxmumLaserShots => maxLasetShots;

    public float LaserDelay => laserDelay;
    public float LaserShotColdown => laserShotColdown;
}
