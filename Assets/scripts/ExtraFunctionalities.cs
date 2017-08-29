using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtraFunctionalities : MonoBehaviour {

	[SerializeField] private KeyCode pauseButton;
	[SerializeField] private KeyCode advanceTimeButton;
	[SerializeField] private KeyCode screenShotButton;
	[SerializeField] private KeyCode sequencedScreenShotButton;
	[SerializeField] private Image advanceTimeImage;
	[SerializeField] private int screenShotResolution = 1;
	[SerializeField] private int sequencedNumber = 5;

	[SerializeField] private bool resetNumber = false;

	private int ssNumber;

	// Use this for initialization
	void Start () {
		if (resetNumber) {
			PlayerPrefs.SetInt ("ssNumber", 0);
			resetNumber = false;
		}

		ssNumber = PlayerPrefs.GetInt ("ssNumber");
		//Debug.Log (ssNumber);

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

		if (Input.GetKeyDown(sequencedScreenShotButton)) {
			StartCoroutine (TakeSequencedScreenShot());
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

		ScreenCapture.CaptureScreenshot ("Screenshots/" + ssNumber + ".png", screenShotResolution); Debug.Log ("Screenshoot taked!");
		ssNumber++;
		PlayerPrefs.SetInt ("ssNumber", ssNumber);
	}

	IEnumerator TakeSequencedScreenShot () {
		for (int i = 0; i < sequencedNumber; i++) {
			TakeScreenShot ();
			yield return new WaitForSeconds (0.5f);
		}

		StopCoroutine ("TakeSequencedScreenShot");
	}
}
