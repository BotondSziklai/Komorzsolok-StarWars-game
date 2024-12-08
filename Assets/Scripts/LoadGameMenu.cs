using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LoadGameMenu : MonoBehaviour
{
    public GameObject loadGamePanel; // Reference to the Load Game panel
    public TMP_Dropdown savedGamesDropdown; // Dropdown for saved levels
    public LevelData[] levelDataArray; // Array of all level data
    public Color playableColor = Color.black; // Color for unlocked levels
    public Color lockedColor = Color.gray; // Color for locked levels

    private void Start()
    {
        PopulateDropdown(); // Populate dropdown on start
    }

    public void ShowLoadGameMenu()
    {
        if (loadGamePanel != null)
        {
            loadGamePanel.SetActive(true); // Show Load Game panel
            PopulateDropdown(); // Refresh dropdown options
        }
    }

    public void CloseLoadGameMenu()
    {
        if (loadGamePanel != null)
        {
            loadGamePanel.SetActive(false); // Hide Load Game panel
        }
    }

    public void LoadSelectedGame()
    {
        if (savedGamesDropdown != null)
        {
            string selectedLevel = savedGamesDropdown.options[savedGamesDropdown.value].text; // Get selected level name

            if (selectedLevel.Contains("(Locked)"))
            {
                Debug.LogWarning($"Level '{selectedLevel}' is locked."); // Prevent loading locked levels
                return;
            }

            LevelData selectedLevelData = FindLevelDataByName(selectedLevel); // Find level data for the selected level

            if (selectedLevelData == null || string.IsNullOrEmpty(selectedLevelData.nextLevelCutscene))
            {
                Debug.LogError($"Invalid level data for: {selectedLevel}"); // Handle missing or invalid data
                return;
            }

            SceneManager.LoadScene(selectedLevelData.nextLevelCutscene); // Load cutscene for the selected level
        }
    }

    private void PopulateDropdown()
    {
        savedGamesDropdown.ClearOptions(); // Clear previous options
        string lastUnlockedLevel = PlayerPrefs.GetString("LastLevel", "Level 1"); // Get last unlocked level

        foreach (LevelData levelData in levelDataArray)
        {
            if (levelData != null)
            {
                bool isUnlocked = IsLevelUnlocked(levelData.levelName, lastUnlockedLevel); // Check if level is unlocked
                string displayText = isUnlocked ? levelData.levelName : $"{levelData.levelName} (Locked)";
                savedGamesDropdown.options.Add(new TMP_Dropdown.OptionData(displayText)); // Add to dropdown
            }
        }

        savedGamesDropdown.RefreshShownValue(); // Refresh dropdown UI
    }

    private bool IsLevelUnlocked(string levelName, string lastUnlockedLevel)
    {
        int selectedIndex = GetLevelIndexByName(levelName);
        int unlockedIndex = GetLevelIndexByName(lastUnlockedLevel);
        return selectedIndex <= unlockedIndex; // Return true if level is unlocked
    }

    private int GetLevelIndexByName(string levelName)
    {
        for (int i = 0; i < levelDataArray.Length; i++)
        {
            if (levelDataArray[i].levelName == levelName) // Match level name
            {
                return i;
            }
        }
        return -1; // Return -1 if level is not found
    }

    private LevelData FindLevelDataByName(string levelName)
    {
        foreach (LevelData data in levelDataArray)
        {
            if (data != null && data.levelName == levelName) // Match level name
            {
                return data;
            }
        }
        return null; // Return null if no match is found
    }
}
