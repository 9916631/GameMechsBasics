using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    public int soundToPlay; // Sound effect to play when the player is hurt

    // Called when another collider enters the trigger zone of this GameObject
    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider has the tag "Player"
        if (other.tag == "Player")
        {
            // Call the Hurt method in the HealthManager to reduce the player's health
            HealthManager.instance.Hurt();

            // Play the specified sound effect using AudioManager
            AudioManager.instance.PlaySFX(soundToPlay);
        }
    }
}
