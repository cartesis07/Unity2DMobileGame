using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class PlayMusic : MonoBehaviour
{

    public AudioSource randomSound;
    public AudioClip[] audioSources;

    private void Update()
    {
        if (!randomSound.isPlaying && randomSound.enabled)
        {
            randomSound.clip = audioSources[Random.Range(0, audioSources.Length)];
            randomSound.Play();
        }
    }

}
