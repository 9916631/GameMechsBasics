using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public GameObject cpOn, cpOff; // Checkpoint visual states
    public int soundToPlay;        // Sound effect to play upon reaching the checkpoint

    // OnTriggerEnter is called when another Collider enters the trigger zone
    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider has the "Player" tag
        if (other.tag == "Player")
        {
            // Set the spawn point to the current checkpoint's position
            GameManager.instance.SetSpawnPoint(transform.position);

            // Find all CheckPoint objects in the scene
            CheckPoint[] allCP = FindObjectsOfType<CheckPoint>();

            // Loop through all checkpoints to update their visual states
            for (int i = 0; i < allCP.Length; i++)
            {
                allCP[i].cpOff.SetActive(true);
                allCP[i].cpOn.SetActive(false);
            }

            // Update the visual state of the current checkpoint
            cpOff.SetActive(false);
            cpOn.SetActive(true);
        }

        // Play the specified sound effect using AudioManager
        AudioManager.instance.PlaySFX(soundToPlay);
    }
}
