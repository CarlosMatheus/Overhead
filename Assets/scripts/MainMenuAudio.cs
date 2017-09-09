using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuAudio : MonoBehaviour {

    private AudioManager audioManager;

	void Start () 
    {
        audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            audioManager.StopWithFade("MusicMainScene", 1f);
            audioManager.SetVolume("MainMenuMusic", 0.7f);
            audioManager.PlayWithFade("MainMenuMusic", 2f);
        }
        else
        {
            audioManager.StopWithFade("MusicMainScene", 2f);
            audioManager.StopWithFade("MainMenuMusic", 2f);
            print("aqui");
        }
            
	}
}
