using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtraFunctionalities : MonoBehaviour {

	public KeyCode pauseButton;
	public KeyCode advanceTimeButton;
	public KeyCode screenShotButton;
	public Image advanceTimeImage;

	private int ssNumber;

	// Use this for initialization
	void Start () {
		ssNumber = PlayerPrefs.GetInt ("ssNumber"); Debug.Log (ssNumber);

		if (ssNumber == null)
			ssNumber = 0;

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

		if (Input.GetKeyDown(screenShotButton)) {
			TakeScreenShot ();
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

	public void TakeScreenShot() {

		ScreenCapture.CaptureScreenshot ("Screenshots/" + ssNumber + ".png", 2); Debug.Log ("Screenshoot taked!");
		ssNumber++;
		PlayerPrefs.SetInt ("ssNumber", ssNumber);
	}
}
