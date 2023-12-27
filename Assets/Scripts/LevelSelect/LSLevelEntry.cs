// This script represents an entry point for a level in the LevelSelect scene.
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LSLevelEntry : MonoBehaviour
{
    // Public variables for level information.
    public string levelName, levelToCheck, displayName;

    // Private variables for level status and UI elements.
    private bool canLoadLevel, levelUnlocked;
    public GameObject mapPointactive, mapPointInactive;
    private bool levelLoading;

    // Start method is called before the first frame update.
    void Start()
    {
        Debug.Log("LSLevelEntry Start method called.");
        // Check if PlayerController.instance is not null before accessing its properties.
        if (PlayerController.instance != null)
        {
            Debug.Log("PlayerController.instance is not null.");

            if (PlayerPrefs.GetInt(levelToCheck + "_unlocked") == 1 || levelToCheck == "")
            {
                mapPointactive.SetActive(true);
                mapPointInactive.SetActive(false);
                levelUnlocked = true;
            }
            else
            {
                mapPointactive.SetActive(false);
                mapPointInactive.SetActive(true);
                levelUnlocked = false;
            }

            if (PlayerPrefs.GetString("CurrentLevel") == levelName)
            {
                // Check if transform and position are not null before using them.
                if (transform != null && PlayerController.instance.transform != null)
                {
                    Debug.Log("Transform is not null.");
                    PlayerController.instance.transform.position = transform.position;
                    LSResetPosition.instance.respawnPosition = transform.position;
                }
                else
                {
                    Debug.LogError("Transform is null.");
                }
            }
        }
        else
        {
            Debug.LogError("PlayerController.instance is null. Make sure it is properly assigned.");
        }
    }


    // Update method is called once per frame.
    void Update()
    {
        // Check for jump input to load the level if conditions are met.
        if (Input.GetButtonDown("Jump") && canLoadLevel && levelUnlocked && !levelLoading)
        {
            StartCoroutine(LevelLoadCo());
            levelLoading = true;
            Debug.Log("Jump in the Update method LSEntry script");
        }
    }

    // Called when another collider enters the trigger collider.
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            canLoadLevel = true;
            LSUIManager.instance.lnamePanel.SetActive(true);
            LSUIManager.instance.lnameText.text = displayName;

            // Display the number of coins if available; otherwise, show "???".
            if (PlayerPrefs.HasKey(levelName + "_coins"))
            {
                LSUIManager.instance.coinsText.text = PlayerPrefs.GetInt(levelName + "_coins").ToString();
                Debug.Log("Display number of coins");
            }
            else
            {
                LSUIManager.instance.coinsText.text = "???";
                Debug.Log("otherwise show default UI");
            }
        }
    }

    // Called when another collider exits the trigger collider.
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canLoadLevel = false;
            LSUIManager.instance.lnamePanel.SetActive(false);
            Debug.Log("Exited the collider");
        }
    }

    // Coroutine to handle the level loading process.
    public IEnumerator LevelLoadCo()
    {
        // Stop player movement and initiate a fade to black.
        PlayerController.instance.stopMove = true;
        UIManager.instance.fadeToBlack = true;

        // Wait for a short duration before loading the level.
        yield return new WaitForSeconds(2f);

        // Load the specified level and store it as the current level.
        SceneManager.LoadScene(levelName);
        PlayerPrefs.SetString("CurrentLevel", levelName);
        Debug.Log("level loaded" + levelName);
    }
}
