using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    public float minY; // Minimum Y position for random movement
    public float maxY; // Maximum Y position for random movement
    public float minSpeed = 1f; // Minimum speed for movement
    public float maxSpeed = 3f; // Maximum speed for movement
    public GameObject laserPrefab; // Laser prefab to be fired
    public Transform firePoint; // Position where the laser spawns
    public float fireInterval = 2f; // Time between shots

    public int maxHealth = 20; // Max health for the boss
    private int currentHealth; // Current health of the boss
    private bool hasBoostedFireRate = false; // Whether the fire rate has been boosted
    public float boostedFireInterval = 1f; // Fire rate when boosted
    public GameObject explosionPrefab; // Explosion effect on death
    public GameObject healthbar; // The health bar object

    private float targetY; // Target Y position
    private float speed; // Current speed
    private float nextMoveTime; // Time when the boss should pick a new position
    private float nextFireTime; // Time when the next shot will be fired

    void Start()
    {
        // Set initial health
        currentHealth = maxHealth;

        // Initialize movement and firing timers
        SetRandomTargetPositionAndSpeed();
        nextMoveTime = Time.time + 1f; // Boss will move every second
        nextFireTime = Time.time + fireInterval;
    }

    void Update()
    {
        // Check if it's time to move
        if (Time.time >= nextMoveTime)
        {
            SetRandomTargetPositionAndSpeed();
            nextMoveTime = Time.time + 1f; // Schedule the next movement in 1 second
        }

        // Smoothly move the boss to the target position
        MoveBoss();

        // Fire lasers at regular intervals
        FireLaser();
    }

    private void MoveBoss()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, targetY), step);
    }

    private void SetRandomTargetPositionAndSpeed()
    {
        // Generate a random target position within bounds
        targetY = Random.Range(minY, maxY);
        speed = Random.Range(minSpeed, maxSpeed); // Set a random speed
    }

    private void FireLaser()
    {
        if (Time.time >= nextFireTime)
        {
            if (laserPrefab != null && firePoint != null)
            {
                Instantiate(laserPrefab, firePoint.position, Quaternion.identity);
            }

            nextFireTime = Time.time + fireInterval;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Update the health bar if it exists
        if (healthbar != null)
        {
            Healthbar healthbarComponent = healthbar.GetComponent<Healthbar>();
            if (healthbarComponent != null)
            {
                healthbarComponent.SetHealth(currentHealth);
            }
        }

        // If health drops below 50%, increase fire rate
        if (currentHealth <= maxHealth / 2 && !hasBoostedFireRate)
        {
            BoostFireRate();
        }

        // If health is 0, destroy the boss
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void BoostFireRate()
    {
        hasBoostedFireRate = true;
        fireInterval = boostedFireInterval;
        Debug.Log("Boss firing rate increased!");
    }

    private void Die()
    {
        // Explosion effect
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        // Destroy the health bar
        if (healthbar != null)
        {
            Destroy(healthbar);
        }

        // Destroy the boss
        Destroy(gameObject);
    }
}
