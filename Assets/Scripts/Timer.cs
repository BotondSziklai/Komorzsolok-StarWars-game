using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText; // Reference to the TextMeshProUGUI component
    private float elapsedTime; // The elapsed time in seconds

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        elapsedTime = 0f; // Initialize elapsedTime
    }

    // Update is called once per frame
    void Update()
    {
        // Increase elapsed time by the time taken to complete the last frame
        elapsedTime += Time.deltaTime;

        // Calculate minutes and seconds
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        // Display the formatted time (MM:SS)
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
