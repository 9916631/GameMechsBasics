using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    // Sound to play when the player is killed
    public int soundToPlay;

    // OnTriggerEnter is called when another Collider enters the trigger zone
    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider has the "Player" tag
        if (other.tag == "Player")
        {
            // Call the Respawn method from the GameManager
            GameManager.instance.Respawn();

            // Play the specified sound effect using AudioManager
            AudioManager.instance.PlaySFX(soundToPlay);
        }
    }
}
