using UnityEngine;
using UnityEngine.UI;

public class PlayerLives : MonoBehaviour
{
    public int lives = 3; // Total lives
    public Image[] livesUI; // UI elements to represent lives
    public GameObject explosionPrefab ;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Optional: Ensure all livesUI elements are enabled at the start
        for (int i = 0; i < livesUI.Length; i++)
        {
            livesUI[i].enabled = i < lives;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Enemy")
        {
            Destroy(collision.collider.gameObject); // Destroy the enemy
            
            lives -= 1; // Decrease the player's lives

            // Update the lives UI
            for (int i = 0; i < livesUI.Length; i++)
            {
                if (i < lives)
                {
                    livesUI[i].enabled = true; // Show remaining lives
                }
                else
                {
                    livesUI[i].enabled = false; // Hide lost lives
                }
            }

            // Check if lives are exhausted
            if (lives <= 0)
            {
                Destroy(gameObject); // Destroy the player's ship
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}
