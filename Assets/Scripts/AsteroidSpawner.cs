using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public float minSpawnInterval = 1f;
    public float maxSpawnInterval = 3f;
    public float asteroidSpeed = 5f;
    public float spawnYMin = -3f;
    public float spawnYMax = 3f;
    public float minScale = 0.5f;
    public float maxScale = 1.5f;
    public int minAsteroidsPerSpawn = 1;
    public int maxAsteroidsPerSpawn = 3;
    public float spawnBuffer = 1f; // Minimum distance between spawned asteroids on the y-axis

    private float screenRightEdge;
    private List<float> recentSpawnYPositions = new List<float>();

    void Start()
    {
        screenRightEdge = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x + 1f;
        StartCoroutine(SpawnAsteroids());
    }

    IEnumerator SpawnAsteroids()
    {
        while (true)
        {
            float spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(spawnInterval);

            int asteroidsToSpawn = Random.Range(minAsteroidsPerSpawn, maxAsteroidsPerSpawn + 1);

            for (int i = 0; i < asteroidsToSpawn; i++)
            {
                SpawnAsteroid();
            }

            // Clear the recent positions list after a delay to prevent over-population
            yield return new WaitForSeconds(0.5f);
            recentSpawnYPositions.Clear();
        }
    }

    void SpawnAsteroid()
    {
        float spawnY;
        int attempts = 0;
        const int maxAttempts = 5;
        bool positionFound;

        // Attempt to find a non-overlapping spawnY
        do
        {
            spawnY = Random.Range(spawnYMin, spawnYMax);
            positionFound = true;

            foreach (float recentY in recentSpawnYPositions)
            {
                if (Mathf.Abs(spawnY - recentY) < spawnBuffer)
                {
                    positionFound = false;
                    break;
                }
            }
            attempts++;
        } while (!positionFound && attempts < maxAttempts);

        // Only spawn if a position was found
        if (positionFound)
        {
            Vector3 spawnPosition = new Vector3(screenRightEdge, spawnY, 0);
            GameObject asteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);

            // Randomize size and rotation
            float randomScale = Random.Range(minScale, maxScale);
            asteroid.transform.localScale = new Vector3(randomScale, randomScale, 1);

            float randomRotation = Random.Range(0f, 360f);
            asteroid.transform.rotation = Quaternion.Euler(0, 0, randomRotation);

            // Add a floating effect with slight rotation
            float rotationSpeed = Random.Range(-20f, 20f);
            asteroid.AddComponent<AsteroidRotation>().rotationSpeed = rotationSpeed;

            asteroid.GetComponent<Rigidbody2D>().linearVelocity = Vector2.left * asteroidSpeed;

            // Store this position in the recent list
            recentSpawnYPositions.Add(spawnY);
        }
    }
}

public class AsteroidRotation : MonoBehaviour
{
    public float rotationSpeed;

    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
