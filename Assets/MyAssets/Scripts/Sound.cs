using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public AudioClip clip;

    public string name;
    [Range(0f, 1f)]
    public float volume;
    [Range(0.5f, 1.5f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;

    public void Play()
    {
        source.Play();
    }
}
