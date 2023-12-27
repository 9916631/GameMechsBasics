using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform theDoor;       // Reference to the door's Transform component
    public Transform openRot;       // Rotation when the door is fully open
    public float openSpeed;         // Speed at which the door opens
    private Quaternion startRot;    // Initial rotation of the door when closed
    public ButtonController theButton;  // Reference to the ButtonController script for interaction

    void Start()
    {
        startRot = transform.rotation;  // Store the initial rotation of the door when the game starts
    }

    void Update()
    {
        // Check if the associated button is pressed
        if (theButton.isPressed)
        {
            // If pressed, smoothly rotate the door towards the openRot rotation
            theDoor.rotation = Quaternion.Slerp(theDoor.rotation, openRot.rotation, openSpeed * Time.deltaTime);
        }
        else
        {
            // If not pressed, smoothly rotate the door back to its initial closed position
            theDoor.rotation = Quaternion.Slerp(theDoor.rotation, startRot, openSpeed * Time.deltaTime);
        }
    }
}
