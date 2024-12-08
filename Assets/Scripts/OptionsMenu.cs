using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    public Button cantinaBandButton; // Button to play the Cantina Band music
    public Button imperialAttackButton; // Button to play the Imperial Attack music
    public Button throneRoomButton; // Button to play the Throne Room music
    public Slider volumeSlider; // Slider to adjust the music volume
    public TMP_Text volumePercentageText; // Text displaying the current volume percentage
    public Button restoreButton; // Button to restore default settings

    private void Start()
    {
        // Assign music play actions to each button
        cantinaBandButton.onClick.AddListener(() => MusicManager.Instance.PlayMusic(MusicManager.Instance.musicClips[0]));
        imperialAttackButton.onClick.AddListener(() => MusicManager.Instance.PlayMusic(MusicManager.Instance.musicClips[1]));
        throneRoomButton.onClick.AddListener(() => MusicManager.Instance.PlayMusic(MusicManager.Instance.musicClips[2]));

        // Initialize volume slider with the current music volume
        volumeSlider.value = MusicManager.Instance.audioSource.volume;
        volumeSlider.onValueChanged.AddListener(OnVolumeSliderChanged);

        // Assign restore defaults action to the restore button
        restoreButton.onClick.AddListener(() => {
            MusicManager.Instance.RestoreDefaults();
            volumeSlider.value = 0.5f; // Reset slider to default value
        });

        UpdateVolumeText(volumeSlider.value); // Update the volume text to match the slider
    }

    private void OnVolumeSliderChanged(float value)
    {
        MusicManager.Instance.SetVolume(value); // Adjust the music volume
        UpdateVolumeText(value); // Update the displayed volume percentage
    }

    private void UpdateVolumeText(float value)
    {
        int percentage = Mathf.RoundToInt(value * 100); // Convert volume to percentage
        volumePercentageText.text = percentage + "%"; // Display the percentage
    }
}
