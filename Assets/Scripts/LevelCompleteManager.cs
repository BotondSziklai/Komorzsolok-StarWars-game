using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteManager : MonoBehaviour
{
    public GameObject levelCompleteCanvas; // Reference to the level complete UI canvas
    public LevelData currentLevelData; // Current level data
    public LevelData nextLevelData; // Next level data

    // Saves progress to unlock the next level
    public void SaveProgress()
    {
        if (nextLevelData != null)
        {
            PlayerPrefs.SetString("LastLevel", nextLevelData.levelName);
            Debug.Log("Progress saved: " + nextLevelData.levelName);
        }
        else
        {
            Debug.LogError("NextLevelData is missing!");
        }
    }

    // Loads the main menu
    public void BackToMainMenu()
    {
        Time.timeScale = 1; // Resume game
        SceneManager.LoadScene("Main Menu");
    }

    // Loads the next level
    public void LoadNextLevel()
    {
        Time.timeScale = 1; // Resume game
        if (nextLevelData != null)
        {
            Debug.Log("Loading next level: " + nextLevelData.levelName);
            PlayerPrefs.SetString("LastLevel", nextLevelData.levelName); // Save progress

            if (!string.IsNullOrEmpty(nextLevelData.nextLevelCutscene))
            {
                SceneManager.LoadScene(nextLevelData.nextLevelCutscene); // Load next level cutscene
            }
            else
            {
                Debug.LogError("Next level cutscene is missing!");
            }
        }
        else
        {
            Debug.LogError("NextLevelData is missing!");
        }
    }

    // Shows the level complete UI and pauses the game
    public void ShowLevelComplete()
    {
        if (levelCompleteCanvas != null)
        {
            levelCompleteCanvas.SetActive(true); // Activate canvas
            Time.timeScale = 0; // Pause game
            Debug.Log("Level Complete UI displayed.");
        }
        else
        {
            Debug.LogError("LevelCompleteCanvas is missing!");
        }

        SaveProgress(); // Save progress when the level is completed
    }
}
