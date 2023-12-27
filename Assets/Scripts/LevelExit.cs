using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    // Animator for handling animations
    public Animator anim;

    // OnTriggerEnter is called when another Collider enters the trigger zone
    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider has the "Player" tag
        if (other.tag == "Player")
        {
            // Trigger the "Hit" animation in the attached Animator component
            anim.SetTrigger("Hit");

            // Start the level end coroutine from the GameManager
            StartCoroutine(GameManager.instance.levelEndCo());
        }
    }
}
