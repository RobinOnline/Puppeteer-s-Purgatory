using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private Sound[] musicSounds, effectSounds;
    [SerializeField] private AudioSource musicSource, effectSource;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }
    public void ChangeMusicVolume(float value)
    {
        musicSource.volume = value;
    }
    public void ChangeEffectsVolume(float value)
    {
        effectSource.volume = value;
    }
    public void PlayMusic(string title)
    {
        foreach (Sound sound in musicSounds)
        {
            if (sound.title == title)
            {
                musicSource.clip = sound.audioClip;
                musicSource.Play();
            }
        }
    }
    public void PlayEffect(string title)
    {
        foreach (Sound sound in effectSounds)
        {
            if (sound.title == title)
            {
                effectSource.PlayOneShot(sound.audioClip);
            }
        }
    }
    public void ToggleEffects()
    {
        effectSource.mute = !effectSource.mute;
    }
    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }
}
