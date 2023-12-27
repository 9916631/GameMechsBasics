/*
Instantiate:
Description: Instantiate is a method in Unity used to create a new instance (clone) of a specified GameObject or prefab during runtime.
Purpose: The purpose of Instantiate is to dynamically spawn new instances of GameObjects or prefabs at specified positions and rotations in the game world. In the given example (Instantiate(pickupEffect, transform.position, transform.rotation);), it is creating a new instance of the pickupEffect GameObject at the current position (transform.position) and rotation (transform.rotation) of the GameObject containing this script.
This is often used for effects like spawning particles, explosions, or other visual elements. 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount;          // Amount of health to heal
    public bool isFullHeal;         // Flag indicating whether it's a full heal
    public GameObject pickupEffect; // Effect to instantiate upon pickup
    public int soundToPlay;         // Sound effect to play upon pickup

    // OnTriggerEnter is called when another Collider enters the trigger zone
    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider has the "Player" tag
        if (other.tag == "Player")
        {
            // Destroy the pickup object upon player collision
            Destroy(gameObject);
            Debug.Log("this is for the destroy game object heart");

            // Check if it's a full heal
            if (isFullHeal)
            {
                // Reset the player's health to maximum
                HealthManager.instance.ResetHealth();
            }
            else
            {
                // Add the specified amount of health to the player
                HealthManager.instance.AddHealth(healAmount);
            }

            // Instantiate the pickup effect at the current position and rotation
            Instantiate(pickupEffect, transform.position, transform.rotation);

            // Play the specified sound effect using AudioManager
            AudioManager.instance.PlaySFX(soundToPlay);
        }
    }
}
