using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    public LevelData nextLevelData; // Data for the next level
    public float minY; // Minimum Y position for movement
    public float maxY; // Maximum Y position for movement
    public float minSpeed = 1f; // Minimum movement speed
    public float maxSpeed = 3f; // Maximum movement speed
    public GameObject laserPrefab; // Laser prefab for attacks
    public Transform firePoint; // Laser spawn point
    public float fireInterval = 2f; // Time between laser shots

    public int maxHealth = 20; // Maximum health of the boss
    private int currentHealth; // Current health of the boss
    public int GetCurrentHealth()
    {
        return currentHealth;
    }
    private bool hasBoostedFireRate = false; // Tracks if fire rate has been boosted
    public float boostedFireInterval = 1f; // Fire interval after boost
    public GameObject explosionPrefab; // Explosion effect on death
    public GameObject healthbar; // Boss health bar UI

    private float targetY; // Target Y position for movement
    private float speed; // Current movement speed
    private float nextMoveTime; // Next time to change position
    private float nextFireTime; // Next time to fire a laser

    void Start()
    {
        currentHealth = maxHealth; // Initialize health
        SetRandomTargetPositionAndSpeed(); // Set initial movement
        nextMoveTime = Time.time + 1f; // Initial movement delay
        nextFireTime = Time.time + fireInterval; // Initial fire delay
    }

    void Update()
    {
        if (Time.time >= nextMoveTime)
        {
            SetRandomTargetPositionAndSpeed(); // Change position
            nextMoveTime = Time.time + 1f; // Reset movement timer
        }

        MoveBoss(); // Move the boss
        FireLaser(); // Handle firing
    }

    private void MoveBoss()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, targetY), step);
    }

    private void SetRandomTargetPositionAndSpeed()
    {
        targetY = Random.Range(minY, maxY); // Set random Y position
        speed = Random.Range(minSpeed, maxSpeed); // Set random speed
    }

    private void FireLaser()
    {
        if (Time.time >= nextFireTime && laserPrefab != null && firePoint != null)
        {
            Instantiate(laserPrefab, firePoint.position, Quaternion.identity); // Fire laser
            nextFireTime = Time.time + fireInterval; // Reset fire timer
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reduce health

        if (healthbar != null)
        {
            Healthbar healthbarComponent = healthbar.GetComponent<Healthbar>();
            if (healthbarComponent != null)
            {
                healthbarComponent.SetHealth(currentHealth); // Update health bar
            }
        }

        if (currentHealth <= maxHealth / 2 && !hasBoostedFireRate)
        {
            BoostFireRate(); // Increase fire rate at half health
        }

        if (currentHealth <= 0)
        {
            Die(); // Destroy boss if health is 0
        }
    }

    private void BoostFireRate()
    {
        hasBoostedFireRate = true;
        fireInterval = boostedFireInterval; // Reduce fire interval
        Debug.Log("Boss firing rate increased!");
    }

    public GameObject levelCompleteUI; // Level completion UI
    public LevelData currentLevelData; // Current level data

    private void Die()
    {
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity); // Spawn explosion effect
        }

        LevelCompleteManager levelCompleteManager = FindObjectOfType<LevelCompleteManager>();
        if (levelCompleteManager != null)
        {
            levelCompleteManager.ShowLevelComplete(); // Notify level completion
        }
        else
        {
            Debug.LogError("LevelCompleteManager not found!");
        }

        if (healthbar != null)
        {
            Destroy(healthbar); // Remove health bar
        }

        Destroy(gameObject); // Destroy the boss
    }
}
