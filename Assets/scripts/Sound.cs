using UnityEngine.Audio;
using UnityEngine;


//this is marked serizlizable so we can maske it appears in unity editor with the AudioManager
[System.Serializable]
public class Sound {

    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;

}
