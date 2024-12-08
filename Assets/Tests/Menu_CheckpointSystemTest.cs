using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;
using TMPro;

public class CheckpointSystemTest
{
    [UnityTest]
    public IEnumerator LoadGameMenu_LockedAndUnlockedLevels()
    {
        PlayerPrefs.SetString("LastLevel", "Level 2");
        PlayerPrefs.Save();

        yield return SceneManager.LoadSceneAsync("Main Menu");

        var playButton = GameObject.Find("PLAY")?.GetComponent<Button>();
        Assert.IsNotNull(playButton, "PLAY button not found.");

        playButton.onClick.Invoke();
        yield return null;

        var loadGameButton = GameObject.Find("Load Game")?.GetComponent<Button>();
        Assert.IsNotNull(loadGameButton, "Load Game button not found.");

        loadGameButton.onClick.Invoke();
        yield return null;

        var loadGamePanel = GameObject.Find("LoadGamePanel");
        Assert.IsNotNull(loadGamePanel, "Load Game panel not found.");

        var dropdown = loadGamePanel.GetComponentInChildren<TMP_Dropdown>();
        Assert.IsNotNull(dropdown, "Dropdown not found in Load Game panel.");

        dropdown.RefreshShownValue();
        var options = dropdown.options;
        Assert.AreEqual("Level 1", options[0].text);
        Assert.AreEqual("Level 2", options[1].text);
        Assert.IsTrue(options[2].text.Contains("(Locked)"), "Level 3 should be locked.");
    }
}
