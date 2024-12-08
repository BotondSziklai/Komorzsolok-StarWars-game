using UnityEngine;
using UnityEngine.UI;

public class HowToPlayMenu : MonoBehaviour
{
    public GameObject gameplayContent; // Gameplay tab content
    public GameObject controlsContent; // Controls tab content
    public GameObject strategyContent; // Strategy tab content

    public Button gameplayTab; // Button for gameplay tab
    public Button controlsTab; // Button for controls tab
    public Button strategyTab; // Button for strategy tab

    public Button closeButton; // Button to close the menu

    private string activeTab; // Tracks the currently active tab

    private void Start()
    {
        // Assign tab button click events
        gameplayTab.onClick.AddListener(() => SwitchTab("Gameplay"));
        controlsTab.onClick.AddListener(() => SwitchTab("Controls"));
        strategyTab.onClick.AddListener(() => SwitchTab("Strategy"));

        // Assign close button event
        closeButton.onClick.AddListener(CloseMenu);

        SwitchTab("Gameplay"); // Default to gameplay tab
    }

    public void SwitchTab(string tabName)
    {
        activeTab = tabName; // Set active tab

        // Disable all content panels
        gameplayContent.SetActive(false);
        controlsContent.SetActive(false);
        strategyContent.SetActive(false);

        // Enable the selected tab's content
        switch (tabName)
        {
            case "Gameplay":
                gameplayContent.SetActive(true);
                break;
            case "Controls":
                controlsContent.SetActive(true);
                break;
            case "Strategy":
                strategyContent.SetActive(true);
                break;
            default:
                Debug.LogError($"Unknown tab: {tabName}");
                break;
        }

        // Update tab button interactivity
        gameplayTab.interactable = tabName != "Gameplay";
        controlsTab.interactable = tabName != "Controls";
        strategyTab.interactable = tabName != "Strategy";
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false); // Hide the menu
    }
}
