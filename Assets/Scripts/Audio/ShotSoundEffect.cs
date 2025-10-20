using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotSoundEffect : MonoBehaviour
{
    [SerializeField] private AudioClip _shotSound;

    public void Play()
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = _shotSound;
        audioSource.Play();
    }
}
