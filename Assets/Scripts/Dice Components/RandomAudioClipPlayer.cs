using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class RandomAudioClipPlayer : MonoBehaviour
{
    [SerializeField] AudioClip[] _clips;

    AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    [ContextMenu("Play Random Clip")]
    public void PlayRandomClip()
    {
        _audioSource.PlayOneShot(_clips[Random.Range(0, _clips.Length)]);
    }
}