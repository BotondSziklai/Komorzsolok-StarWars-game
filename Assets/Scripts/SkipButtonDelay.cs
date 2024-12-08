using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SkipButtonDelay : MonoBehaviour
{
    public CanvasGroup skipButtonCanvasGroup;
    public VideoPlayer videoPlayer;
    public float delay = 5f; // Time to wait before enabling the skip button
    public float fadeDuration = 1f; // Duration of the fade-in effect for the skip button

    void Start()
    {
        if (skipButtonCanvasGroup != null)
        {
            // Initialize the skip button as invisible and non-interactable
            skipButtonCanvasGroup.alpha = 0;
            skipButtonCanvasGroup.interactable = false;
            skipButtonCanvasGroup.blocksRaycasts = false;
            StartCoroutine(FadeInSkipButton()); // Start the coroutine to fade in the skip button
        }

        if (videoPlayer != null)
        {
            // Add an event listener for when the video ends
            videoPlayer.loopPointReached += OnVideoEnd;
        }
    }

    private IEnumerator FadeInSkipButton()
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay

        float timer = 0f; // Initialize a timer for the fade-in effect
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            skipButtonCanvasGroup.alpha = Mathf.Lerp(0, 1, timer / fadeDuration);
            yield return null;
        }

        // The button is fully visible and interactable
        skipButtonCanvasGroup.alpha = 1;
        skipButtonCanvasGroup.interactable = true;
        skipButtonCanvasGroup.blocksRaycasts = true;
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        LoadMainMenu(); // Load the main menu when the video ends
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
