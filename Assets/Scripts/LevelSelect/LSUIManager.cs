// This script manages the UI (User Interface) for the LevelSelect scene.
using UnityEngine;
using UnityEngine.UI;

public class LSUIManager : MonoBehaviour
{
    // Create a static instance variable to ensure only one instance of this class exists.
    public static LSUIManager instance;

    // Public variables for UI elements.
    public Text lnameText;      // Text element to display the level name.
    public GameObject lnamePanel;  // Panel containing the level name.
    public Text coinsText;      // Text element to display the number of coins.
   
    private void Awake()
    {
        // Set the static instance variable to this instance of the LSUIManager class.
        instance = this;
        Debug.Log("LSUImanager has awokon");
    }
}