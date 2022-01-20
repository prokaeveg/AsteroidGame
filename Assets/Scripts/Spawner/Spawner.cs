using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner
{
    float nextSpawnTime = 0.0f;
    public void CreateObject(GameObject asteroid, Vector3 asteroidSpawnPoint ,GameObject enemy, Transform[] spawnPoints, float spawnRate, float asteroidSpawnChance,
                            float SpawnDistance, float trajectoryVariance, float asteroidMinSize, float AsteroidMaxSize)
    {
        if (Time.time > nextSpawnTime)
        {
            float spawnChance = Random.Range(0, 100);
            int spawnPoint = Random.Range(0, spawnPoints.Length);

            if (spawnChance >= asteroidSpawnChance)
            {
                CreateEnemy(enemy, spawnPoints[spawnPoint]);
            }
            else
            {
                CreateAsteroid(asteroid, asteroidSpawnPoint, trajectoryVariance, asteroidMinSize, AsteroidMaxSize);
            }
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    private void CreateEnemy(GameObject enemy, Transform position)
    {
        GameObject Enemy = Object.Instantiate(enemy, position.position, position.rotation);
    }

    private void CreateAsteroid(GameObject asteroid, Vector3 spawnPoint, float trajectoryVariance, float minSize, float maxSize)
    {
        float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
        Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);
        Vector2 spawnDirection = Random.insideUnitCircle.normalized * 0.4f;
        Vector2 trajectory = rotation * spawnDirection;
        var size = Random.Range(minSize, maxSize);

        GameObject Asteroid = Object.Instantiate(asteroid, spawnPoint, rotation);
        Asteroid.GetComponent<Asteroid>().SetTrajectory(trajectory);
        Asteroid.GetComponent<Asteroid>().Size = size;

    }
}
