using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotSoundEffect : MonoBehaviour
{
    [SerializeField] private AudioClip _shotSound;

    public void Play()
    {
        StartCoroutine(PlaySound(_shotSound));
    }

    private IEnumerator PlaySound(AudioClip audioClip)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play();
        yield return new WaitForSeconds(audioClip.length);
        Destroy(audioSource);
        yield return true;
    }
}
