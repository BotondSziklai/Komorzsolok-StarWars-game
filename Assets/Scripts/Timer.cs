using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject pauseMenu; // The entire Pause Menu panel
    [SerializeField] private Button continueButton;
    [SerializeField] private Button backToMainMenuButton;

    [Header("Boss Settings")]
    [SerializeField] private GameObject boss; // Reference to the boss GameObject in the scene
    [SerializeField] private GameObject healthbar; // Reference to the health bar GameObject

    [Header("Asteroid Spawner")]
    [SerializeField] private AsteroidSpawner asteroidSpawner;

    private float elapsedTime;
    private bool bossSpawned = false;
    private bool isPaused = false;
    private bool isGameOver = false; // Track if the game is over

    void Start()
    {
        elapsedTime = 0f;

        if (pauseMenu != null) pauseMenu.SetActive(false); // Pause Menu starts inactive

        if (continueButton != null)
        {
            continueButton.onClick.AddListener(ResumeGame); // Set up the Continue button
        }

        if (backToMainMenuButton != null)
        {
            backToMainMenuButton.onClick.AddListener(BackToMainMenu); // Set up the Back to Main Menu button
        }

        // Boss and health bar invisible at the start
        if (boss != null) boss.SetActive(false);
        if (healthbar != null) healthbar.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }

        if (isPaused || isGameOver) return; // Stop updates if paused or Game Over

        elapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (elapsedTime >= 60f && !bossSpawned)
        {
            StopAsteroidSpawner();
            SpawnBoss();
        }
    }

    private void StopAsteroidSpawner()
    {
        if (asteroidSpawner != null)
        {
            asteroidSpawner.StopSpawning();
            asteroidSpawner.enabled = false;
        }
    }

    private void SpawnBoss()
    {
        if (boss != null)
        {
            boss.SetActive(true); // Activate the boss
        }
        if (healthbar != null)
        {
            healthbar.SetActive(true); // Activate the health bar
        }
        bossSpawned = true;
    }

    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;

        if (pauseMenu != null) pauseMenu.SetActive(true); // Show the pause menu
    }

    private void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;

        if (pauseMenu != null) pauseMenu.SetActive(false); // Hide the pause menu
    }

    private void BackToMainMenu()
    {
        Time.timeScale = 1f; // Reset time scale before leaving the game
        SceneManager.LoadScene("Main Menu"); // Load the Main Menu scene
    }

    public void StopTimer()
    {
        isGameOver = true; // Stop the timer when the game ends
    }
}
