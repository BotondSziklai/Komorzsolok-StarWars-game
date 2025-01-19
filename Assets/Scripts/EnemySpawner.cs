using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Tie Fighter Settings")]
    public GameObject tieFighterPrefab; // Reference to the Tie Fighter prefab
    public float minSpawnInterval = 2f; // Minimum time between spawns
    public float maxSpawnInterval = 4f; // Maximum time between spawns
    public float enemySpeed = 3f; // Speed of enemies
    public float spawnYMin = -3f;
    public float spawnYMax = 3f;
    
    private float screenRightEdge;
    private bool isSpawning = true;

    void Start()
    {
        screenRightEdge = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x + 1f;
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (isSpawning)
        {
            float spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(spawnInterval);
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        float spawnY = Random.Range(spawnYMin, spawnYMax);
        Vector3 spawnPosition = new Vector3(screenRightEdge, spawnY, 0);
        GameObject enemy = Instantiate(tieFighterPrefab, spawnPosition, Quaternion.identity);

        Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.left * enemySpeed;
        }
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }
}
