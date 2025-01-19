using UnityEngine;

public class LaserMovement : MonoBehaviour
{
    public float speed = 10f; // Speed of the laser
    public GameObject explosionPrefab;
    public GameObject healthPickupPrefab; // Health pickup prefab
    public float healthDropChance = 0.1f; // chance to drop health

    void Update()
    {
        // Move the laser in the forward direction (right direction as per your script)
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the laser collides with an object tagged as "Enemy" (like an asteroid or Tie Fighter)
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Instantiate the explosion at the position of the enemy (not the laser)
            Instantiate(explosionPrefab, collision.transform.position, Quaternion.identity);

            // chance to spawn a health pickup when an enemy is destroyed
            if (Random.value < healthDropChance)
            {
                GameObject healthPickup = Instantiate(healthPickupPrefab, collision.transform.position, Quaternion.identity);
                Debug.Log("Health Pickup létrehozva: " + healthPickup.name);

                HealthPickup hpScript = healthPickup.GetComponent<HealthPickup>();
                if (hpScript != null)
                {
                    hpScript.SetSpeed(3f);
                    Debug.Log("Health Pickup sebessége beállítva.");
                }
                else
                {
                    Debug.LogError("HealthPickup script HIÁNYZIK az instanciált objektumon!");
                }
            }

            // Destroy the enemy
            Destroy(collision.gameObject);

            // Destroy the laser
            Destroy(gameObject);
        }

        // If the laser collides with a boundary
        else if (collision.gameObject.CompareTag("BoundaryLaser"))
        {
            Destroy(gameObject); // Destroy the laser if it hits a boundary
        }

        // If the laser collides with the boss
        else if (collision.CompareTag("Boss"))
        {
            BossBehavior boss = collision.GetComponent<BossBehavior>();
            if (boss != null)
            {
                boss.TakeDamage(1); // Deal 1 damage to the boss for each hit
            }

            Destroy(gameObject); // Destroy the laser
        }
    }
}
