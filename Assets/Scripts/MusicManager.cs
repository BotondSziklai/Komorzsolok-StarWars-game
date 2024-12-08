using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance; // Singleton instance for global access

    public AudioSource audioSource; // AudioSource to play music
    public AudioClip defaultClip; // Default music clip
    public AudioClip[] musicClips; // Array of available music clips
    private float defaultVolume = 0.5f; // Default volume level

    private void Awake()
    {
        // Set up the singleton instance
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep the manager persistent across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    private void Start()
    {
        audioSource.volume = defaultVolume; // Set the initial volume
        PlayMusic(defaultClip); // Start playing the default music
    }

    public void PlayMusic(AudioClip clip)
    {
        // Play the new music clip if it is not already playing
        if (audioSource.clip != clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume; // Adjust the music volume
    }

    public void RestoreDefaults()
    {
        PlayMusic(defaultClip); // Reset to default music
        SetVolume(defaultVolume); // Reset to default volume
    }
}
