using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicToggle : MonoBehaviour
{
    public Toggle toggle;
    public AudioSource audioSource;
    public AudioClip backgroundMusic;

    void Start()
    {
        // Assign the background music clip
        audioSource.clip = backgroundMusic;

        // Play the background music
        audioSource.Play();

        // Add a listener to the toggle's value change event
        toggle.onValueChanged.AddListener(delegate {
            ToggleMusic(toggle.isOn); // Pass the toggle state directly
        });
    }

    void ToggleMusic(bool isMuted)
    {
        audioSource.mute = isMuted;
    }
}
