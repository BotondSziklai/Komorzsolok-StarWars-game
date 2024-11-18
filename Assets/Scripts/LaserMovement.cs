using UnityEngine;

public class LaserMovement : MonoBehaviour
{
    public float speed = 10f; // Speed of the laser
    public GameObject explosionPrefab;

    void Update()
    {
        // Move the laser in the forward direction (right direction as per your script)
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the laser collides with an object tagged as "Enemy" (like an asteroid)
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Instantiate the explosion at the position of the asteroid (not the laser)
            Instantiate(explosionPrefab, collision.transform.position, Quaternion.identity);

            // Destroy the asteroid (enemy)
            Destroy(collision.gameObject);

            // Destroy the laser
            Destroy(gameObject);
        }

        // If the laser collides with a boundary
        if (collision.gameObject.CompareTag("BoundaryLaser")) 
        {
            Destroy(gameObject); // Destroy the laser if it hits a boundary
        }
    }
}
