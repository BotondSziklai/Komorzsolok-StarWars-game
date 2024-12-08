using UnityEngine;
using UnityEngine.UI;

public class PlayerLives : MonoBehaviour
{
    public int lives = 3; // Player's initial number of lives
    public Image[] livesUI; // UI elements representing the player's lives
    public GameObject explosionPrefab; // Prefab for the explosion effect
    public GameManager gameManager; // Reference to the GameManager script
    private bool isDead = false; // Flag to ensure death logic runs only once

    private void Start()
    {
        UpdateLivesUI(); // Set up the lives UI to match the initial lives count
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Destroy(collision.collider.gameObject); // Remove the enemy from the scene

            lives -= 1; // Decrease the player's lives
            UpdateLivesUI(); // Refresh the lives UI

            if (lives <= 0 && !isDead)
            {
                isDead = true;
                HandlePlayerDeath(); // Handle logic for player death
            }
        }
    }

    private void UpdateLivesUI()
    {
        for (int i = 0; i < livesUI.Length; i++)
        {
            // Enable or disable life icons based on remaining lives
            livesUI[i].enabled = i < lives;
        }
    }

    private void HandlePlayerDeath()
    {
        // Instantiate explosion effect at the player's position
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject); // Remove the player from the scene

        // Notify the GameManager to trigger the Game Over logic
        if (gameManager != null)
        {
            gameManager.TriggerGameOver();
        }
    }
}
