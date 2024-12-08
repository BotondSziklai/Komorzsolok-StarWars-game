using UnityEngine;

public class ShootingFromWings : MonoBehaviour
{
    public GameObject laserPrefab; // The laser prefab to be instantiated when shooting
    public Transform leftWingTip; // The position of the left wing tip where the laser will spawn
    public Transform rightWingTip; // The position of the right wing tip where the laser will spawn

    void Update()
    {
        // Check if the "Fire1" button is pressed
        if (Input.GetButtonDown("Fire1"))
        {
            // Get the positions of the left and right wing tips
            Vector3 leftWingPosition = leftWingTip.position;
            Vector3 rightWingPosition = rightWingTip.position;

            // Instantiate a laser at each wing tip position
            Instantiate(laserPrefab, leftWingPosition, Quaternion.identity); // Spawn laser at left wing
            Instantiate(laserPrefab, rightWingPosition, Quaternion.identity); // Spawn laser at right wing
        }
    }
}
