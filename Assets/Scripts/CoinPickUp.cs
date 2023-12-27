using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    public int value;              // Value of the coins collected
    public GameObject pickupEffect; // Effect to instantiate upon pickup
    public int soundToPlay;         // Sound effect to play upon pickup

    // OnTriggerEnter is called when another Collider enters the trigger zone
    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider has the "Player" tag
        if (other.tag == "Player")
        {
            // Add the coin value to the player's total coins
            GameManager.instance.AddCoins(value);

            // Destroy the coin object upon player collision
            Destroy(gameObject);
            Debug.Log("destroyed the coins");

            // Instantiate the pickup effect at the current position and rotation
            Instantiate(pickupEffect, transform.position, transform.rotation);

            // Play the specified sound effect using AudioManager
            AudioManager.instance.PlaySFX(soundToPlay);
        }
    }
}