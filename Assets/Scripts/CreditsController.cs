using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CreditsController : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Reference to the video player
    public Button pausePlayButton; // Button to toggle pause/play

    public Sprite pauseSprite; // Default pause icon
    public Sprite playSprite; // Default play icon
    public Sprite pauseHoverSprite; // Hover state pause icon
    public Sprite playHoverSprite; // Hover state play icon

    private bool isPaused = false; // Tracks the pause state

    void Start()
    {
        pausePlayButton.onClick.AddListener(TogglePausePlay); // Add button listener
        UpdateButtonSprites(); // Initialize button sprites
    }

    void TogglePausePlay()
    {
        isPaused = !isPaused; // Toggle pause state

        if (isPaused)
        {
            videoPlayer.Pause(); // Pause the video
        }
        else
        {
            videoPlayer.Play(); // Play the video
        }

        UpdateButtonSprites(); // Update button visuals
    }

    void UpdateButtonSprites()
    {
        SpriteState spriteState = new SpriteState(); // Create new sprite state

        if (isPaused)
        {
            pausePlayButton.GetComponent<Image>().sprite = playSprite; // Set play icon
            spriteState.highlightedSprite = playHoverSprite; // Set hover play icon
            spriteState.pressedSprite = playSprite; // Set pressed play icon
        }
        else
        {
            pausePlayButton.GetComponent<Image>().sprite = pauseSprite; // Set pause icon
            spriteState.highlightedSprite = pauseHoverSprite; // Set hover pause icon
            spriteState.pressedSprite = pauseSprite; // Set pressed pause icon
        }

        pausePlayButton.spriteState = spriteState; // Apply sprite state to button
        pausePlayButton.targetGraphic.SetAllDirty(); // Force UI update
    }
}
