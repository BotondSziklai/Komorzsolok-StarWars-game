using UnityEngine;

public class ShootingFromWings : MonoBehaviour
{
    public GameObject laserPrefab; // Laser prefab
    public Transform leftWingTip; // Reference to the left wing tip spawn point
    public Transform rightWingTip;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // Spawn lasers at the wing tips, accounting for the ship's position and rotation
            Vector3 leftWingPosition = leftWingTip.position;
            Vector3 rightWingPosition = rightWingTip.position;


            // Instantiate lasers at the calculated positions with the ship's rotation
            Instantiate(laserPrefab, leftWingPosition, Quaternion.identity);
            Instantiate(laserPrefab, rightWingPosition, Quaternion.identity);

        }
    }
}
