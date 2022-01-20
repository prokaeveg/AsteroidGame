using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour
{
    [SerializeField]private float spawnRate = 1.0f;
    [SerializeField]private float spawnDistance = 12.0f;
    [SerializeField]private GameObject asteroid, enemy;
    [Range(0.0f, 45.0f)] public float trajectoryVariance = 15.0f;
    [SerializeField, Min(0.0f)] private float minSize = 3f;
    [SerializeField, Min(0.0f)] private float maxSize = 10f;

    [SerializeField, Range(0.0f, 100f)]private float asteroidSpawnChance = 75f;
    [SerializeField]private Transform[] enemySpawnPoints;

    private Spawner spawner = null;

    private void Awake()
    {
        spawner = new Spawner();
    }
    private void Update()
    {
        Vector2 spawnDirection = Random.insideUnitCircle.normalized * 0.5f;
        Vector3 spawnPoint = spawnDirection * this.spawnDistance;
        spawnPoint += transform.position;
        
        spawner.CreateObject(asteroid, spawnPoint, enemy, enemySpawnPoints, spawnRate, asteroidSpawnChance, spawnDistance, trajectoryVariance, minSize, maxSize);
    }


}
