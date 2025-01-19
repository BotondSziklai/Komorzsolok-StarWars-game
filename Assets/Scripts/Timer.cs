using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button backToMainMenuButton;

    [Header("Boss Settings")]
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject healthbar;

    [Header("Spawners")]
    [SerializeField] private AsteroidSpawner asteroidSpawner;
    [SerializeField] private EnemySpawner enemySpawner; // Hozzáadva az EnemySpawner

    private float elapsedTime;
    private bool bossSpawned = false;
    private bool isPaused = false;
    private bool isGameOver = false;

    void Start()
    {
        elapsedTime = 0f;

        if (pauseMenu != null) pauseMenu.SetActive(false);

        if (continueButton != null)
        {
            continueButton.onClick.AddListener(ResumeGame);
        }

        if (backToMainMenuButton != null)
        {
            backToMainMenuButton.onClick.AddListener(BackToMainMenu);
        }

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

        if (isPaused || isGameOver) return;

        elapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (elapsedTime >= 60f && !bossSpawned)
        {
            StopAllSpawners();
            SpawnBoss();
        }
    }

    private void StopAllSpawners()
    {
        if (asteroidSpawner != null)
        {
            asteroidSpawner.StopSpawning();
            asteroidSpawner.enabled = false;
        }

        if (enemySpawner != null) // Leállítja a Tie Fighterek spawnolását is
        {
            enemySpawner.StopSpawning();
            enemySpawner.enabled = false;
        }
    }

    private void SpawnBoss()
    {
        if (boss != null)
        {
            boss.SetActive(true);
        }
        if (healthbar != null)
        {
            healthbar.SetActive(true);
        }
        bossSpawned = true;
    }

    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;

        if (pauseMenu != null) pauseMenu.SetActive(true);
    }

    private void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;

        if (pauseMenu != null) pauseMenu.SetActive(false);
    }

    private void BackToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    public void StopTimer()
    {
        isGameOver = true;
    }
}
