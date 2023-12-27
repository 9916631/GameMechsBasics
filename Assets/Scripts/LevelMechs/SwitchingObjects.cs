using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchingObjects : MonoBehaviour
{
    public GameObject theObject;         // Reference to the GameObject to be switched
    public ButtonController theButton;   // Reference to the ButtonController script for interaction
    public bool revealWhenPressed;       // Determines whether to reveal the object when the button is pressed

    // Called every frame
    void Update()
    {
        // Check if the associated button is pressed
        if (theButton.isPressed)
        {
            // If pressed, set theObject's active state based on revealWhenPressed flag
            theObject.SetActive(revealWhenPressed);
        }
        else
        {
            // If not pressed, set theObject's active state based on the opposite of revealWhenPressed flag
            theObject.SetActive(!revealWhenPressed);
        }
    }
}

