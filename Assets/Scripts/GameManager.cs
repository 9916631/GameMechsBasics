/*
Vector3:
Description: Vector3 is a structure in Unity representing a point or direction in 3D space.
Purpose: Used for representing positions, directions, and movements in a 3D environment.

Cursor:
Description: Cursor is a class in Unity for controlling the mouse cursor.
Purpose: Used to show or hide the cursor, and set its behavior, like locking it to the screen.

Time:
Description: Time is a class in Unity for managing time-related functions.
Purpose: Used for tracking the game's frame rate, controlling animations, and managing time-dependent events.

Quaternion:
Description: Quaternion is a class in Unity for representing rotations.
Purpose: Used to handle 3D rotations in a more efficient and stable way than Euler angles.

Physics:
Description: Physics is a class in Unity for handling physics-related operations.
Purpose: Used for controlling and simulating physical interactions, such as collisions, rigid body dynamics, and raycasting.

Input:
Description: Input is a class in Unity for handling user input.
Purpose: Used to capture and process keyboard, mouse, touch, and controller input, allowing the game to respond to user actions.
 */


/*
IEnumerator is an interface in C# used for iterating over a collection of objects one at a time.
IEnumerator is often used in coroutines to write asynchronous code.
Coroutines with IEnumerator allow for the execution of code that appears to run concurrently without using threads.
Coroutines can yield control back to the calling code, enabling the execution to be paused and resumed at specific points.
Common use cases in Unity include animations, timed actions, and other asynchronous operations.
 */

/*
 Asynchronous code is a programming paradigm that allows tasks to execute independently of the main program flow, 
enabling concurrent operations without blocking the execution. It's particularly useful for handling tasks that may take time, 
such as fetching data from a server or performing I/O operations, without freezing the user interface or the entire program.
Asynchronous code often involves callbacks, promises, or async/await constructs to manage the flow of execution and handle 
the results of asynchronous tasks.
 */

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    // Singleton instance of GameManager
    public static GameManager instance;

    // Variables for respawn positions, death effect, coins, music, and level to load
    private Vector3 respawnPosition;
    public GameObject deathEffect;
    public int currentCoins;
    public int levelEndMusic = 8;
    public string levelToLoad;
    public bool isRespawning;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Set the instance to this object, creating a singleton
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (PlayerController.instance == null)
        {
            // Find the Player object in the scene
            PlayerController.instance = FindObjectOfType<PlayerController>();
        }

        respawnPosition = PlayerController.instance.transform.position;
        AddCoins(0);
    }

    // Update is called once per frame
    void Update()
    {
        // Check for the Esc key to pause/unpause the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    // Method to respawn the player
    public void Respawn()
    {
        // Start the respawn coroutine
        StartCoroutine(RespawnCo());

        // Inform the HealthManager that the player was killed
        HealthManager.instance.PlayerKilled();

        // Set the player's position to the respawn position
        PlayerController.instance.transform.position = respawnPosition;
    }
    
    // Coroutine for respawning the player
    public IEnumerator RespawnCo()
    {
        // Deactivate the player and disable the camera's Cinemachine brain
        PlayerController.instance.gameObject.SetActive(false);
        CameraController.instance.theCMBrain.enabled = false;

        // Trigger the fade to black effect
        UIManager.instance.fadeToBlack = true;

        // Instantiate the death effect at the player's position
        Instantiate(deathEffect, PlayerController.instance.transform.position + new Vector3(0f, 1f, 0f), PlayerController.instance.transform.rotation);

        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);

        // Set the respawning flag to true
        isRespawning = true;

        // Reset the player's health and trigger the fade from black effect
        HealthManager.instance.ResetHealth();
        UIManager.instance.fadeFromBlack = true;

        // Set the player's position to the respawn position
        PlayerController.instance.transform.position = respawnPosition;

        // Enable the camera's Cinemachine brain and reactivate the player
        CameraController.instance.theCMBrain.enabled = true;
        PlayerController.instance.gameObject.SetActive(true);
    }


    // Method to set a new spawn point
    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        respawnPosition = newSpawnPoint;
        Debug.Log("Spawn point set");
    }

    // Method to add coins and update the UI
    public void AddCoins(int coinsToAdd)
    {
        currentCoins += coinsToAdd;
        UIManager.instance.cointText.text = "" + currentCoins;
    }

    // Method to pause or unpause the game
    public void PauseUnpause()
    {
        if (UIManager.instance.pauseScreen.activeInHierarchy)
        {
            // If the pause screen is active, hide it, resume time, and lock the cursor
            UIManager.instance.pauseScreen.SetActive(false);
            Time.timeScale = 1f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            // If the pause screen is not active, show it, pause time, and unlock the cursor
            UIManager.instance.pauseScreen.SetActive(true);
            UIManager.instance.closeOptions();
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    // Coroutine for handling the end of a level
    public IEnumerator levelEndCo()
    {
        // Play the level end music, stop player movement, and trigger the fade to black effect
        AudioManager.instance.PlayMusic(levelEndMusic);
        PlayerController.instance.stopMove = true;
        UIManager.instance.fadeToBlack = true;

        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);

        // Log a message indicating the level has ended
        Debug.Log("level ended");

        // Unlock the next level and save the player's coin count
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_unlocked", 1);
        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_coins"))
        {
            if (currentCoins > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_coins"))
            {
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_coins", currentCoins);
            }
        }
        else
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_coins", currentCoins);
        }

        // Load the specified level
        SceneManager.LoadScene(levelToLoad);
    }
}

