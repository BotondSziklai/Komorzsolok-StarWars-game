using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI; // Reference to the Game Over UI panel
    private bool isGameOver = false; // To prevent multiple triggers

    private void Start()
    {
        // Ensure the Game Over UI is hidden at the start
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }
        else
        {
            Debug.LogError("Game Over UI is not assigned in the Inspector!");
        }
    }

    public void TriggerGameOver()
    {
        if (isGameOver) return; // Prevent multiple Game Over triggers

        isGameOver = true;

        // Delay the Game Over screen to allow for animations or effects
        Invoke(nameof(ShowGameOverScreen), 0.5f); // Optional delay (adjust if needed)
    }

    private void ShowGameOverScreen()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true); // Show the Game Over UI
        }
        else
        {
            Debug.LogError("Game Over UI is not assigned!");
        }

        Time.timeScale = 0f; // Pause the game
    }

    public void RetryGame()
    {
        Time.timeScale = 1f; // Reset time scale
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f; // Reset time scale
        SceneManager.LoadScene("Main Menu"); // Load the Main Menu scene
    }
}
