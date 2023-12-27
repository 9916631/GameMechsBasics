/*
Sprite:
Description: In computer graphics and game development, a sprite is a 2D image or animation that is integrated into a larger scene.
Purpose: Sprites are used to represent characters, objects, and visual elements in 2D games. They can be moved, rotated, and 
scaled to create dynamic and visually appealing scenes. In game development, sprites are often used for characters, backgrounds,
items, and other graphical elements that contribute to the overall visual experience of the game.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    // Singleton instance of the HealthManager
    public static HealthManager instance;

    // Health variables
    public int currentHealth, maxHealth;
    public float invincibalLength = 2;
    private float invincCounter;

    // Array of health bar images
    public Sprite[] healthBarImages;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Set the instance to this object, creating a singleton
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initialize health
        ResetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is invincible
        if (invincCounter > 0)
        {
            // Decrease the invincibility counter
            invincCounter -= Time.deltaTime;

            // Flash the player pieces while invincible
            for (int i = 0; i < PlayerController.instance.PlayerPieces.Length; i++)
            {
                if (Mathf.Floor(invincCounter * 5f) % 2 == 0)
                {
                    PlayerController.instance.PlayerPieces[i].SetActive(true);
                }
                else
                {
                    PlayerController.instance.PlayerPieces[i].SetActive(false);
                }

                // Make sure player pieces are active when invincibility ends
                if (invincCounter <= 0)
                {
                    PlayerController.instance.PlayerPieces[i].SetActive(true);
                }
            }
        }
    }

    // Method to handle player taking damage
    public void Hurt()
    {
        // Check if the player is not invincible
        if (invincCounter <= 0)
        {
            // Decrease health
            currentHealth--;

            // Check if the player has run out of health
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                // Respawn the player
                GameManager.instance.Respawn();
            }
            else
            {
                // Apply knockback to the player
                PlayerController.instance.KnockBack();
                // Set invincibility period
                invincCounter = invincibalLength;
            }

            // Update the UI
            UpdateUI();
        }
    }

    // Method to reset health to maximum
    public void ResetHealth()
    {
        currentHealth = maxHealth;
        // Enable the health image in the UI
        UIManager.instance.healthImage.enabled = true;
        // Update the UI
        UpdateUI();
    }

    // Method to add health
    public void AddHealth(int amountToHeal)
    {
        // Increase health
        currentHealth += amountToHeal;

        // Ensure health doesn't exceed maximum
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        // Update the UI
        UpdateUI();
    }

    // Method to update the UI based on current health
    public void UpdateUI()
    {
        // Update the health text in the UI
        UIManager.instance.healthText.text = currentHealth.ToString();

        // Set the appropriate health bar image based on current health
        switch (currentHealth)
        {
            case 5:
                UIManager.instance.healthImage.sprite = healthBarImages[4];
                break;
            case 4:
                UIManager.instance.healthImage.sprite = healthBarImages[3];
                break;
            case 3:
                UIManager.instance.healthImage.sprite = healthBarImages[2];
                break;
            case 2:
                UIManager.instance.healthImage.sprite = healthBarImages[1];
                break;
            case 1:
                UIManager.instance.healthImage.sprite = healthBarImages[0];
                break;
            case 0:
                // Disable the health image when health is zero
                UIManager.instance.healthImage.enabled = false;
                break;
        }
    }

    // Method to handle the player being killed
    public void PlayerKilled()
    {
        // Set health to zero and update the UI
        currentHealth = 0;
        UpdateUI();
    }
}
