/*
Image:
Description: In Unity, an Image is a UI component used to display a 2D graphic or sprite on the screen.
Purpose: Images are commonly used for UI elements, icons, buttons, and other visual elements in user interfaces. They allow developers to display graphics and textures within the UI canvas to enhance the visual presentation of a game or application.

TextMeshProUGUI:
Description: TextMeshProUGUI is a Unity component used for rendering high-quality text in a User Interface (UI).
Purpose: TextMeshProUGUI is an advanced text rendering solution that provides improved text clarity, rich styling options, and support for various languages and character sets. It is commonly used for displaying dynamic text content in UI elements like labels, buttons, and other textual components.

Slider:
Description: The Slider is a UI component in Unity that allows users to select a value from a range by moving a draggable handle along a track.
Purpose: Sliders are useful for user interactions involving continuous or discrete numeric values, such as volume controls, brightness adjustments, or any scenario where a user needs to specify a value within a specified range. They provide a visual representation of the selectable range and the current selected value 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    // Singleton instance of the UIManager
    public static UIManager instance;

    // UI elements
    public Image blackScreen;
    public float fadeSpeed = 2f;
    public bool fadeToBlack, fadeFromBlack;

    public TextMeshProUGUI healthText;
    public Image healthImage;
    public TextMeshProUGUI cointText;
    public GameObject pauseScreen, optionsScreen;

    public Slider musicVolSlider, sfxVolSlider;
    public string levelSelect, mainMenu;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Set the instance to this object, creating a singleton
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if fading to black
        if (fadeToBlack)
        {
            // Update the alpha of the black screen to create a fade effect
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            // Check if fade to black is complete
            if (blackScreen.color.a == 1f)
            {
                fadeToBlack = false;
            }
        }

        // Check if fading from black
        if (fadeFromBlack)
        {
            // Update the alpha of the black screen to create a fade out effect
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            // Check if fade from black is complete
            if (blackScreen.color.a == 0f)
            {
                fadeFromBlack = false;
            }
        }
    }

    // Method to resume the game
    public void Resume()
    {
        GameManager.instance.PauseUnpause();
    }

    // Method to open options screen
    public void OpenOptions()
    {
        optionsScreen.SetActive(true);
    }

    // Method to close options screen
    public void closeOptions()
    {
        optionsScreen.SetActive(false);
    }

    // Method to load the level select scene
    public void LevelSelect()
    {
        SceneManager.LoadScene(levelSelect);
        Time.timeScale = 1f; // Resume time scale
        Debug.Log("Loadscene loaded");
    }

    // Method to load the main menu scene
    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f; // Resume time scale
        Debug.Log("MainMenuscene loaded");
    }

    // Method to set the music volume
    public void SetMusicLevel()
    {
        AudioManager.instance.SetMusicLevel();
    }

    // Method to set the sound effects volume
    public void SFXLevel()
    {
        AudioManager.instance.SetSFXLevel();
    }
}
