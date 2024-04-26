using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleMute : MonoBehaviour
{
    public AudioSource audioSource;

    public void ToggleMuteState()
    {
        audioSource.mute = !audioSource.mute;
    }
}
