﻿using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    
    [SerializeField] private Sound[] sounds;

    public static AudioManager instance;

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogError("Sound: " + name + "not found!");
            return;
        }

        s.source.Play();
    }

    //Set sounds that will play as the game starts:
    private void Start()
    {
        Play("MusicMainScene");
    }

    private void Awake () 
    {
        //Prevent of having two audioManager in one scene:
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }    

        //this is to not destroy this audio manager in the change of scene!
        DontDestroyOnLoad(gameObject);

        //if there is no sound dont do anything
        if (sounds == null)
            return;

        foreach(Sound s in sounds)
        {

            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
	}
}
