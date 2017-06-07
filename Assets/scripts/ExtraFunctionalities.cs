using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraFunctionalities : MonoBehaviour {

	public KeyCode pauseButton;
	public KeyCode advanceTimeButton;
	public KeyCode showAllHPButton;
	public KeyCode closeStore;
	public KeyCode quitGame;
	public KeyCode setVolumeUp;
	public KeyCode setVolumeDown;

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (pauseButton)) {
			if (Time.timeScale > 1)
				Time.timeScale = 1;

			Time.timeScale = 1 - Time.timeScale;
		}

		if (Time.timeScale != 0) {   // If it's not paused
			if (Input.GetKey (advanceTimeButton)) {
				Time.timeScale = 2;
			} else {
				Time.timeScale = 1;
			}
		}

		if (Input.GetKeyDown (quitGame)) {
			Application.Quit ();
		}
	}
}
