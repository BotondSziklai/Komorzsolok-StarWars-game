using UnityEngine;
using UnityEngine.UI;

public class PlayerLives : MonoBehaviour
{
    public int lives = 3; // Player's initial number of lives
    public Image[] livesUI; // UI elements to represent lives
    public GameObject explosionPrefab; // Explosion effect prefab
    public GameOverManager gameOverManager; // Reference to the GameOverManager script
    private bool isDead = false; // To ensure death logic is executed once

    private void Start()
    {
        // Ensure lives UI matches the player's initial lives
        UpdateLivesUI();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Destroy(collision.collider.gameObject); // Destroy the enemy

            // Decrease lives
            lives -= 1;

            // Update the lives UI
            UpdateLivesUI();

            // Handle player death
            if (lives <= 0 && !isDead)
            {
                isDead = true;
                HandlePlayerDeath();
            }
        }
    }

    private void UpdateLivesUI()
    {
        for (int i = 0; i < livesUI.Length; i++)
        {
            if (i < lives)
            {
                livesUI[i].enabled = true; // Show life icon
            }
            else
            {
                livesUI[i].enabled = false; // Hide life icon
            }
        }
    }

    private void HandlePlayerDeath()
    {
        // Trigger explosion
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        // Destroy the player game object
        Destroy(gameObject);

        // Notify the GameOverManager to handle the game over logic
        if (gameOverManager != null)
        {
            gameOverManager.TriggerGameOver();
        }
        else
        {
            Debug.LogWarning("GameOverManager is not assigned in the PlayerLives script!");
        }
    }
}
