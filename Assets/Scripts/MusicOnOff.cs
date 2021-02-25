using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicOnOff : MonoBehaviour
{

    public AudioSource audioMusicSource;

    // Start is called before the first frame update
    void Awake()
    {
        audioMusicSource = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audioMusicSource.enabled = !audioMusicSource.enabled;
        }
    }
}
