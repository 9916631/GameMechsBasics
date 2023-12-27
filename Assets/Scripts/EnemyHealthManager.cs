using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public int maxiumHealth = 1;          // Maximum health of the enemy
    private int currentHealth;            // Current health of the enemy
    public int deathSound;                // Sound effect to play on enemy death
    public GameObject deathEffect;        // Particle effect to spawn on enemy death
    public GameObject itemToDrop;         // Item to drop on enemy death

    void Start()
    {
        currentHealth = maxiumHealth;     // Initialize current health to maximum health when the enemy is created
    }

    // Function to handle taking damage by the enemy
    public void TakeDamage()
    {
        currentHealth--;                 // Decrease the current health by 1 when the enemy takes damage

        // Check if the enemy's health has reached zero or below
        if (currentHealth <= 0)
        {
            AudioManager.instance.PlaySFX(deathSound);  // Play the death sound effect using AudioManager
            Destroy(gameObject);                        // Destroy the enemy GameObject

            // Trigger a bounce effect on the player (assuming there's a PlayerController script)
            PlayerController.instance.Bounce();

            // Instantiate a death effect at the enemy's position with an offset for visual effect
            Instantiate(deathEffect, transform.position + new Vector3(0, 1.2f, 0f), transform.rotation);

            // Instantiate an item drop at the enemy's position with an offset
            Instantiate(itemToDrop, transform.position + new Vector3(0, .5f, 0f), transform.rotation);
        }
    }
}
