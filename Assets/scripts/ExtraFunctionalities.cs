using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtraFunctionalities : MonoBehaviour {

	public KeyCode pauseButton;
	public KeyCode advanceTimeButton;
	public Image advanceTimeImage;

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		if (advanceTimeImage != null)
			advanceTimeImage.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (pauseButton)) {
			if (Time.timeScale > 1)
				Time.timeScale = 1;

			Time.timeScale = 1 - Time.timeScale;
		}

		if (Time.timeScale != 0) {   // If it's not paused
			if (Input.GetKeyDown (advanceTimeButton)) {

				if (advanceTimeImage != null)
					advanceTimeImage.enabled = !advanceTimeImage.enabled;

				if (Time.timeScale == 1) {
					Time.timeScale = 3;
					return;
				}

				if (Time.timeScale > 1) {
					Time.timeScale = 1;
					return;
				}
			}
		}
	}
}
