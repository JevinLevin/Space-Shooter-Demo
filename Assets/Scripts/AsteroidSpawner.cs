using System;
using System.Collections;
using System.Collections.Generic;
using Mathsfx;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = Mathsfx.Vector3;


public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject asteroidPrefab;
    [SerializeField] private Vector2 spawnDelay;
    [SerializeField] private Vector2 spawnDistance;


    private void Start()
    {
        StartCoroutine(nameof(SpawnAsteroids));
    }

    private IEnumerator SpawnAsteroids()
    {

        while (true)
        {
            // Generate random position in a circle around the player
            float ang = Random.Range(0, 359) * MathsfxConst.Deg2Rad;
            Vector3 dir = Vector3.RadToVec(ang).Normalized;
            float distance = Random.Range(spawnDistance.x, spawnDistance.y) * (Random.value < 0.5f ? -1 : 1);
            Vector3 spawnPosition = new Vector3(Player.Instance.transform.position) + (dir * distance);
            
            Asteroid newAsteroid = Instantiate(asteroidPrefab, spawnPosition.ToVector3(), Quaternion.identity, transform).GetComponent<Asteroid>();
        
            newAsteroid.Spawn();


            yield return new WaitForSeconds(Random.Range(spawnDelay.x, spawnDelay.y));
        }
        // ReSharper disable once IteratorNeverReturns
    }
}
