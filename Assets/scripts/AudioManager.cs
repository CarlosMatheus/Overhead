using System.Collections;
using System.Collections.Generic;
﻿using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;

    public static AudioManager instance;

    public void Play(string audioName)
    {
        Sound s = Find(audioName);
        s.source.Play();
    }

    public void Stop(string audioName)
    {
        Sound s = Find(audioName);
        s.source.Stop();
    }

    public void PlayWithFade(string audioName, float fadeTime)
    {
        float initialVolume;

        Sound s = Find(audioName);
        s.source.Play();

        initialVolume = s.source.volume;
        s.source.volume = 0;
        StartCoroutine( FadeAudio ( s, initialVolume, fadeTime, "play") );
    }

    public void StopWithFade(string audioName, float fadeTime)
    {
        Sound s = Find(audioName);
        s.source.Stop();

        StartCoroutine(FadeAudio(s, 0, fadeTime, "stop"));
    } 

    public void SetVolume(string audioName, float volume)
    {
        Sound s = Find(audioName);
        s.source.volume = volume;
    }

    public void SetVolumeWithFade(string audioName, float volume, float fadeTime)
    {
        Sound s = Find(audioName);
        StartCoroutine(FadeAudio(s, volume, fadeTime));
    }

    //Set sounds that will play as the game starts:
    private void Start()
    {
        
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

    private Sound Find(string _soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == _soundName);

        if (s == null)
        {
            Debug.LogError("Sound: " + name + "not found!");
            return null;
        }

        return s;
    }

    IEnumerator FadeAudio(Sound _audio,float finalVolume, float time, string otherAction = "nothing")
    {
        if(otherAction == "play")
        {
            _audio.source.Play();
        }

        float numOfParts = time * 10f;
        float deltaVol = (finalVolume - _audio.source.volume) / numOfParts;
        for (int i = 0; i < numOfParts; i ++)
        {
            yield return new WaitForSeconds( time / numOfParts);
            _audio.source.volume += deltaVol;
        }

        if (otherAction == "stop")
        {
            _audio.source.Stop();
        }
    }
}
