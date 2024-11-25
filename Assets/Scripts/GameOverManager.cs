using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI; // Reference to the GameOver panel
    private bool isGameOver = false;

    private void Start()
    {
        // Ensure the Game Over UI is hidden at the start
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }
    }

    public void TriggerGameOver()
    {
        if (isGameOver) return; // Prevent multiple triggers

        isGameOver = true;

        // Delay the display of the Game Over screen to allow the explosion animation to finish
        Invoke(nameof(ShowGameOverScreen), 0.5f); // Delay by 1 second
    }

    private void ShowGameOverScreen()
    {
        // Display the Game Over UI
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

        // Pause the game
        Time.timeScale = 0f;
    }
}
