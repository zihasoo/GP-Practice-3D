using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioClip[] audios;

    AudioSource source;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }

        source = GetComponent<AudioSource>();
    }

    public void AudioStart(int value)
    {
        source.PlayOneShot(audios[value]);
    }
}
