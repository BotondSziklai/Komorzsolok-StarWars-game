using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Slider slider; // The slider UI component

    // Set the maximum health and initialize the health bar
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    // Update the health value on the slider
    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
