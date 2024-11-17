using UnityEngine;

public class LaserMovement : MonoBehaviour
{
    public float speed = 10f; // Speed of the laser

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
            Destroy(collision.gameObject);  // Destroy the asteroid (enemy)
            Destroy(gameObject);            // Destroy the laser
        }

        // If the laser collides with a boundary
        if (collision.gameObject.CompareTag("BoundaryLaser")) 
        {
            Destroy(gameObject); // Destroy the laser if it hits a boundary
        }
    }
}
