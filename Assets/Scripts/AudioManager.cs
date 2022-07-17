using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public AudioMixerGroup mixerGroup;

    // Static Instance
    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound sound in sounds)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.audioClip;
            sound.audioSource.volume = sound.volume;
            sound.audioSource.loop = sound.loop;
            sound.audioSource.playOnAwake = false;
        }
    }

    private void Start()
    {
        // Start Playing BGM
        PlaySound("Menu");
    }

    public void PlaySound(string audioName)
        // Play one sound
    {
        Sound sound = Array.Find(sounds, sound => sound.name == audioName);
        if (sound == null)
            // No sound name in the list
        {
            Debug.LogWarning("Can't Find " + audioName);
            return;
        }
        sound.audioSource.Play();
    }

    public void PlaySound(string prevAudioName, string nextAudioName)
        // Change one sound to another
    {
        Sound prevSound = Array.Find(sounds, sound => sound.name == prevAudioName);
        Sound nextSound = Array.Find(sounds, sound => sound.name == nextAudioName);

        if (prevSound == null || nextSound == null)
        {
            Debug.LogWarning("Can't find one of the given Audio name!");
            return;
        }

        prevSound.audioSource.Stop();
        nextSound.audioSource.Play();
    }

}
