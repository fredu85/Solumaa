//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//[CreateAssetMenu(menuName = "Audio Events/Simple")]
//public class SimpleAudioEvent : AudioEvent
//{
//    public AudioClip[] clips;
//    public float volume;

//    [Range(0,2)]
//    public float pitch;

//    public override void Play(Sound source)
//    {
//        if (clips.Length == 0)
//            return;

//       source.source.clip= clips[Random.Range(0, clips.Length) ];
//       source.source.volume = volume;
//       source.source.pitch = pitch;
//       source.source.Play();

//    }
//}
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;
[CreateAssetMenu(fileName = "Sound", menuName = "ScriptableObjects/Sound", order = 55)]
public class SimpleAudioEvent : AudioEvent
{

    public AudioClip[] clips;

    [Range(0, 5)]
    public float volume;
    public float volumeRange;

    [Range(0,2)]
    public float pitch;
    public float pitchRange;

    public override void Play(AudioSource source)
    {
        if (clips.Length == 0) return;

        source.clip = clips[Random.Range(0, clips.Length)];
        source.volume = Random.Range(volume - volumeRange, volume + volumeRange);
        source.pitch = Random.Range(pitch-pitchRange, pitch+pitchRange);
        source.Play();
    }

    //public void PlayInEditor()
    //{
    //    AudioSource source = FindObjectOfType<AudioSource>();
    //    if (source != null)
    //        Play(source);
    //    else
    //        Debug.Log("No source");
    //}
}