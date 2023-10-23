using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private Sound[] musicSounds, effectSounds;
    [SerializeField] private RandomSound[] randomSounds;
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
            if (sound.title == title)
                effectSource.PlayOneShot(sound.audioClip);
    }

    public void ToggleEffects()
    {
        effectSource.mute = !effectSource.mute;
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void PlayRandomEffect(string id)
    {
        foreach (RandomSound random in randomSounds)
            if (random.id == id)
                if (random.sounds.Length > 0)
                {
                    int index = Random.Range(0, random.sounds.Length);
                    effectSource.PlayOneShot(random.sounds[index].audioClip);
                }
    }
}
