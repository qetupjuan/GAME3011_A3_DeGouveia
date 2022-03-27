using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
        instance = this;
    }

    public void PlayClip(AudioClip clip)
    {
        source.clip = clip;
        source.Play();

        if (!source.isPlaying) { return; }
    }
}