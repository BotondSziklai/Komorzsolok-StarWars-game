using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 4;
    public float hInput;
    public float vInput;

    public GameObject Boundary;
    public GameObject Boundary1;
    public GameObject Boundary2;
    public GameObject Boundary3;

    private float minX, maxX, minY, maxY;

    void Start()
    {
        // Calculate boundary limits based on the positions of Boundary objects
        minX = Mathf.Min(Boundary.transform.position.x, Boundary1.transform.position.x, Boundary2.transform.position.x, Boundary3.transform.position.x);
        maxX = Mathf.Max(Boundary.transform.position.x, Boundary1.transform.position.x, Boundary2.transform.position.x, Boundary3.transform.position.x);
        minY = Mathf.Min(Boundary.transform.position.y, Boundary1.transform.position.y, Boundary2.transform.position.y, Boundary3.transform.position.y);
        maxY = Mathf.Max(Boundary.transform.position.y, Boundary1.transform.position.y, Boundary2.transform.position.y, Boundary3.transform.position.y);
    }

    void Update()
    {
        // Set input according to new controls
        hInput = Input.GetAxisRaw("Vertical"); // W (left) and S (right)
        vInput = Input.GetAxisRaw("Horizontal"); // A (down) and D (up)

        // Calculate new position based on the adjusted controls
        Vector2 newPosition = transform.position;
        newPosition.x += hInput * moveSpeed * Time.deltaTime; // Horizontal movement with W/S
        newPosition.y += vInput * moveSpeed * Time.deltaTime; // Vertical movement with A/D

        // Clamp position within boundaries
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        // Apply the clamped position to the player
        transform.position = newPosition;

        // Optionally, log a message if the player hits a boundary
        if ((hInput != 0 && (transform.position.x == minX || transform.position.x == maxX)) ||
            (vInput != 0 && (transform.position.y == minY || transform.position.y == maxY)))
        {
            Debug.Log("Player movement stopped at boundary");
        }
    }
}
