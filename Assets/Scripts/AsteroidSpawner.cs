using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;  
    public float spawnInterval = 2;   
    public float spawnRangeY = 5;      
    public float asteroidSpeed = 3;    

    private float spawnX;               

    void Start()
    {
        spawnX = Camera.main.ViewportToWorldPoint(new Vector3(1.1f, 0, 0)).x;

        InvokeRepeating("SpawnAsteroid", 1, spawnInterval);
    }

    void SpawnAsteroid()
    {
  
        float randomY = Random.Range(-spawnRangeY, spawnRangeY);
        Vector3 spawnPosition = new Vector3(spawnX, randomY, 0);

        GameObject asteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);

        Rigidbody2D rb = asteroid.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.left * asteroidSpeed;
        }


        Destroy(asteroid, 10); 
    }
}
