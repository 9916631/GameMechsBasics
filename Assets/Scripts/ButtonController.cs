using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public bool isPressed;        // Indicates if the button is currently pressed
    public Transform button;      // Reference to the button's Transform component
    public Transform buttonDown;  // Transform position when the button is pressed
    private Vector3 buttonUp;     // Initial transform position when the button is not pressed
    public bool isOnOff;          // Indicates if the button operates as an on/off switch

    void Start()
    {
        buttonUp = button.position;  // Store the initial position of the button when it is not pressed
    }

    // Called when another collider enters the trigger zone of this GameObject
    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider has the tag "Player"
        if (other.tag == "Player")
        {
            // Check if the button is configured as an on/off switch
            if (isOnOff)
            {
                // If on/off switch, toggle the button state
                if (isPressed)
                {
                    // If currently pressed, move the button to the up position and mark as not pressed
                    button.position = buttonUp;
                    isPressed = false;
                }
                else
                {
                    // If not pressed, move the button to the down position and mark as pressed
                    button.position = buttonDown.position;
                    isPressed = true;
                }
            }
            else
            {
                // If not an on/off switch, only trigger when the button is not pressed
                if (!isPressed)
                {
                    // Move the button to the down position and mark as pressed
                    button.position = buttonDown.position;
                    isPressed = true;
                }
            }
        }
    }
}

