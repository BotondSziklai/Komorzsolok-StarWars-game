using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class MainMenu : MonoBehaviour
{
    public GameObject creditsPanel; // Credits panel object
    public AudioSource mainMenuMusic; // Main menu music source
    public VideoPlayer creditsVideoPlayer; // Video player for the credits

    public Button pausePlayButton; // Pause/Play button
    public Sprite pauseSprite; // Pause icon
    public Sprite playSprite; // Play icon
    private bool isPaused = false; // Tracks video pause state

    public LevelData firstLevelData; // First level data
    public GameObject noSaveGamePanel; // Panel shown when no save is found

    public void StartNewGame()
    {
        PlayerPrefs.DeleteAll(); // Clear saved data
        PlayerPrefs.SetString("LastLevel", firstLevelData.levelName); // Set first level
        SceneManager.LoadScene(firstLevelData.nextLevelCutscene); // Load cutscene for first level
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("LastLevel"))
        {
            string lastLevelName = PlayerPrefs.GetString("LastLevel"); // Get saved level
            LevelData lastLevelData = FindLevelDataByName(lastLevelName); // Find level data

            if (lastLevelData != null && !string.IsNullOrEmpty(lastLevelData.nextLevelCutscene))
            {
                SceneManager.LoadScene(lastLevelData.nextLevelCutscene); // Load cutscene
            }
        }
        else
        {
            if (noSaveGamePanel != null)
            {
                noSaveGamePanel.SetActive(true); // Show "no save" panel
            }
        }
    }

    private LevelData FindLevelDataByName(string levelName)
    {
        LevelData[] allLevelData = Resources.LoadAll<LevelData>(""); // Load all level data

        foreach (LevelData data in allLevelData)
        {
            if (data.levelName == levelName)
            {
                return data; // Return matching level data
            }
        }
        return null; // No match found
    }

    public void CloseNoSaveGamePanel()
    {
        if (noSaveGamePanel != null)
        {
            noSaveGamePanel.SetActive(false); // Hide "no save" panel
        }
    }

    public void QuitGame()
    {
        Application.Quit(); // Quit the application
    }

    public void ShowCredits()
    {
        if (creditsPanel != null)
        {
            creditsPanel.SetActive(true); // Show credits panel
            if (mainMenuMusic != null)
            {
                mainMenuMusic.Stop(); // Stop main menu music
            }
            if (creditsVideoPlayer != null)
            {
                creditsVideoPlayer.Play(); // Play credits video
            }
        }
    }

    public void HideCredits()
    {
        if (creditsPanel != null)
        {
            creditsPanel.SetActive(false); // Hide credits panel
            if (mainMenuMusic != null)
            {
                mainMenuMusic.Play(); // Resume main menu music
            }
            if (creditsVideoPlayer != null)
            {
                creditsVideoPlayer.Stop(); // Stop credits video
            }
        }
    }

    public void TogglePausePlay()
    {
        if (creditsVideoPlayer != null)
        {
            if (isPaused)
            {
                creditsVideoPlayer.Play(); // Resume video
                pausePlayButton.GetComponent<Image>().sprite = pauseSprite; // Set pause icon
            }
            else
            {
                creditsVideoPlayer.Pause(); // Pause video
                pausePlayButton.GetComponent<Image>().sprite = playSprite; // Set play icon
            }
            isPaused = !isPaused; // Toggle pause state
        }
    }
}
