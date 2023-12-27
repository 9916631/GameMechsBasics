/*
AudioManager:
Definition: AudioManager is a class that manages audio-related functionality in a game.
Purpose: It often includes methods for playing background music, sound effects, and adjusting audio settings. It helps centralize audio-related logic, making it easier to control and manage the game's audio.

AudioSource:
Definition: In Unity, AudioSource is a component that allows a GameObject to play sounds.
Purpose: AudioSource is attached to a GameObject, and it can play audio clips (such as music or sound effects). It provides control over volume, pitch, spatial blending, and other audio-related properties.

AudioMixerGroup:
Definition: In Unity, AudioMixerGroup is a component that allows sounds to be routed and mixed together.
Purpose: AudioMixerGroups are used with the Audio Mixer to apply effects, adjust volume levels, and control other audio processing parameters. Sounds that share the same AudioMixerGroup can be controlled collectively, providing a way to manage and process audio in groups. 

Singleton:
Definition: A Singleton is a design pattern that restricts the instantiation of a class to a single instance and provides a global point of access to that instance.
Purpose: Singletons are often used when exactly one object is needed to coordinate actions across the system. They help provide a single point of control for certain functionalities. 
 */

using UnityEngine;
using UnityEngine.Audio;

// Define the AudioManager class inheriting from MonoBehaviour
public class AudioManager : MonoBehaviour
{
    // Singleton instance of AudioManager
    public static AudioManager instance;

    // Arrays of AudioSources for music and sound effects
    public AudioSource[] music;
    public AudioSource[] sfx;

    // Index of the level music to play
    public int levelMusicToPlay;

    // Audio mixer groups for music and sound effects
    public AudioMixerGroup musicMixer, sfxMixer;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Set the instance to this object, creating a singleton
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Play the specified level music on start
        PlayMusic(levelMusicToPlay);
    }

    // Method to play music based on the provided index
    public void PlayMusic(int musicToPlay)
    {
        // Stop all currently playing music
        for (int i = 0; i < music.Length; i++)
        {
            music[i].Stop();
        }

        // Play the specified music
        music[musicToPlay].Play();
    }

    // Method to play a sound effect based on the provided index
    public void PlaySFX(int sfxToPlay)
    {
        // Play the specified sound effect
        sfx[sfxToPlay].Play();
    }

    // Method to set the music volume level based on the UI slider value
    public void SetMusicLevel()
    {
        // Set the music volume in the audio mixer based on the UI slider value
        musicMixer.audioMixer.SetFloat("MusicVol", UIManager.instance.musicVolSlider.value);
    }

    // Method to set the sound effects volume level based on the UI slider value
    public void SetSFXLevel()
    {
        // Set the sound effects volume in the audio mixer based on the UI slider value
        sfxMixer.audioMixer.SetFloat("SfxVol", UIManager.instance.sfxVolSlider.value);
    }
}