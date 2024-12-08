using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutsceneManager : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Reference to the VideoPlayer
    public string nextSceneName; // Name of the next scene

    private void Start()
    {
        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer is not assigned!");
            return;
        }

        // Trigger event when the video ends
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        LoadNextScene(); // Load the next scene when the video finishes
    }

    public void SkipCutscene()
    {
        Debug.Log("Cutscene skipped.");
        LoadNextScene(); // Skip to the next scene
    }

    private void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName); // Load the specified scene
        }
        else
        {
            Debug.LogError("Next scene name is not set!");
        }
    }
}
