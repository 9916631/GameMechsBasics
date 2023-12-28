using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingBranch : MonoBehaviour
{
    public Animator SwingAnimator;
    public string swingTrigger;   

    // Called when another collider enters the trigger zone of the parent GameObject
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Set the trigger to start swinging animation
            SwingAnimator.SetTrigger(swingTrigger);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Set the trigger to transition back to idle animation
            SwingAnimator.SetTrigger("BranchIdle");
        }
    }

    // Called when another collider enters the trigger zone of the child GameObject (branch)
    private void OnTriggerChildEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Call the Hurt method in the HealthManager to reduce the player's health
            HealthManager.instance.Hurt();          
        }
    }
}

