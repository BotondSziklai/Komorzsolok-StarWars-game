using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Asteroid Settings")]
    public GameObject asteroidPrefab; // Reference to the asteroid prefab
    public float minSpawnInterval = 1f; // Minimum time between spawns
    public float maxSpawnInterval = 3f; // Maximum time between spawns
    public float asteroidSpeed = 5f; // Speed of asteroids
    public float spawnYMin = -3f; // Minimum Y-coordinate for spawning
    public float spawnYMax = 3f; // Maximum Y-coordinate for spawning
    public float minScale = 0.5f; // Minimum asteroid size
    public float maxScale = 1.5f; // Maximum asteroid size
    public int minAsteroidsPerSpawn = 1; // Minimum asteroids per spawn
    public int maxAsteroidsPerSpawn = 3; // Maximum asteroids per spawn
    public float spawnBuffer = 1f; // Buffer to avoid overlapping spawns

    private float screenRightEdge; // The right edge of the screen
    private List<float> recentSpawnYPositions = new List<float>(); // Store recent spawn positions
    private bool isSpawning = true; // Control variable for asteroid spawning

    void Start()
    {
        // Calculate the right edge of the screen
        screenRightEdge = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x + 1f;

        // Start the asteroid spawning coroutine
        StartCoroutine(SpawnAsteroids());
    }

    private IEnumerator SpawnAsteroids()
    {
        while (isSpawning)
        {
            // Randomize the spawn interval
            float spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(spawnInterval);

            // Randomize the number of asteroids to spawn
            int asteroidsToSpawn = Random.Range(minAsteroidsPerSpawn, maxAsteroidsPerSpawn + 1);

            for (int i = 0; i < asteroidsToSpawn; i++)
            {
                SpawnAsteroid();
            }

            // Clear the recent spawn positions after a delay
            yield return new WaitForSeconds(0.5f);
            recentSpawnYPositions.Clear();
        }
    }

    private void SpawnAsteroid()
    {
        float spawnY;
        int attempts = 0;
        const int maxAttempts = 5; // Max attempts to find a valid position
        bool positionFound;

        // Try to find a non-overlapping position
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

        // Spawn the asteroid if a position is found
        if (positionFound)
        {
            Vector3 spawnPosition = new Vector3(screenRightEdge, spawnY, 0);
            GameObject asteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);

            // Randomize size and rotation
            float randomScale = Random.Range(minScale, maxScale);
            asteroid.transform.localScale = new Vector3(randomScale, randomScale, 1);

            float randomRotation = Random.Range(0f, 360f);
            asteroid.transform.rotation = Quaternion.Euler(0, 0, randomRotation);

            // Add a slight floating effect
            float rotationSpeed = Random.Range(-20f, 20f);
            AsteroidRotation rotationComponent = asteroid.AddComponent<AsteroidRotation>();
            rotationComponent.rotationSpeed = rotationSpeed;

            // Set asteroid velocity
            Rigidbody2D rb = asteroid.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.left * asteroidSpeed;
            }

            // Store this position to avoid overlap
            recentSpawnYPositions.Add(spawnY);
        }
    }

    public void StopSpawning()
    {
        isSpawning = false; // Stops the spawning loop
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BoundaryBLaser"))
        {
            Destroy(gameObject); // Destroy the laser if it hits a boundary
        }
    }

}

public class AsteroidRotation : MonoBehaviour
{
    public float rotationSpeed; // Rotation speed of the asteroid

    void Update()
    {
        // Rotate the asteroid
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}

