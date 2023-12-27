/*
String:
Definition: In programming, a string is a sequence of characters.
Purpose: Strings are used to represent and manipulate text. They are a fundamental data type in many programming languages.

GameObject:
Definition: In Unity, a GameObject is a fundamental object type that represents items in the scene.
Purpose: GameObjects are the building blocks of a Unity scene. They can represent characters, items, cameras, lights, and more. Components, such as scripts, can be attached to GameObjects to define their behavior.

PlayerPrefs:
Definition: In Unity, PlayerPrefs is a class that provides a way to store and retrieve player preferences between game sessions.
Purpose: PlayerPrefs is often used to save and load simple data, such as high scores, settings, or unlocked levels. It allows developers to persistently store data on the player's device.

SceneManager:
Definition: In Unity, SceneManager is a class that provides methods for loading, unloading, and managing scenes.
Purpose: SceneManager is essential for handling different parts of a game, such as transitioning between levels or menus. It allows developers to organize and control the flow of the game by loading and unloading scenes as needed.  

Debug:
Description: Debug is a class in Unity used for displaying information during development and debugging.
Purpose: Developers use Debug to log messages, warnings, and errors to the console. These messages help track the flow of the 
program, identify issues, and provide feedback on the state of the game during development. The Debug class is instrumental for troubleshooting and understanding how code is executed.
*/

/*
Definition MonoBehaviour:
MonoBehaviour is a class provided by the Unity engine.
It is the base class for all scripts that interact with Unity's GameObjects.

Purpose:
MonoBehaviour is designed to be used with GameObjects in Unity scenes.
Scripts that inherit from MonoBehaviour can contain methods (like Start, Update, and others) that Unity calls automatically in response to certain events in the game's lifecycle.

Lifecycle Methods:
MonoBehaviour scripts can implement various lifecycle methods, such as:
Awake(): Called when the script instance is being loaded.
Start(): Called on the frame when a script is enabled.
Update(): Called every frame.
And many more, including methods for physics updates, collisions, and GUI rendering.

Access to Unity Features:
MonoBehaviour provides access to Unity features and services. For example, it allows scripts to interact with GameObjects, manipulate transforms, access input, manage physics, and more.

Script Execution Order:
Unity processes scripts based on a predefined execution order. The order can be adjusted in the Unity Editor. The execution order determines when specific methods in MonoBehaviour scripts are called during the game's lifecycle.
 */

// Import necessary Unity Engine modules
using UnityEngine;
using UnityEngine.SceneManagement;

// Define the MainMenu class inheriting from MonoBehaviour
public class MainMenu : MonoBehaviour
{
    // Public variables accessible from the Unity Editor
    public string firstLevel, levelSelect;   // Names of the first level and the level selection scene
    public GameObject continueButton;         // Reference to the continue button GameObject
    public string[] levelNames;               // Array of level names

    // Start is called before the first frame update
    void Start()
    {
        // Check if the "Continue" key exists in PlayerPrefs
        if (PlayerPrefs.HasKey("Continue"))
        {
            // If it exists, activate the continueButton GameObject
            continueButton.SetActive(true);
        }
        else
        {
            // If not, reset the player's progress
            ResetProgress();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Update function is currently empty
    }

    // Method to start a new game
    public void NewGame()
    {
        // Load the specified first level
        SceneManager.LoadScene(firstLevel);

        // Set "Continue" to 0 in PlayerPrefs
        PlayerPrefs.SetInt("Continue", 0);

        // Set "CurrentLevel" to the name of the first level in PlayerPrefs
        PlayerPrefs.SetString("CurrentLevel", firstLevel);

        // Reset the player's progress
        ResetProgress();
    }

    // Method to continue the game
    public void Continue()
    {
        // Load the level selection scene
        SceneManager.LoadScene(levelSelect);
    }

    // Method to quit the game
    public void QuitGame()
    {
        // Quit the application
        Application.Quit();

        // Log a message to the console
        Debug.Log("exit game");
    }

    // Method to reset the player's progress
    public void ResetProgress()
    {
        // Loop through each level name in the array
        for (int i = 0; i < levelNames.Length; i++)
        {
            // Set the corresponding level's "unlocked" status to 0 in PlayerPrefs
            PlayerPrefs.SetInt(levelNames[i] + "_unlocked", 0);
        }
    }
}