using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class Sound
{
    public string title;
    public AudioClip audioClip;
}

[System.Serializable]
public class RandomSound
{
    public string id;
    public Sound[] sounds;
}