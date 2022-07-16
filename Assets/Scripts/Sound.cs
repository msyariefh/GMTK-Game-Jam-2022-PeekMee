using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound 
{
    public string name;
    public AudioClip audioClip;

    [Range(0f, 1f)] public float volume;
    public bool loop;
    public bool isBGM;

    [HideInInspector]
    public AudioSource audioSource;
    [HideInInspector]
    public AudioMixerGroup audioMixer;
}
