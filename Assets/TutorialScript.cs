using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour {

	public float startCountdown = 2f;
	public float welcomeTextTime = 5f;
	public float Countdown1 = 1f;


	private GameObject welcomeText;
	private GameObject wasdKeysText;
	private CanvasGroup canvasGrup;

	private void Start(){
		canvasGrup = GameObject.Find ("").GetComponent<CanvasGroup> ();
		welcomeText = GameObject.Find("WelcomeText");
		wasdKeysText = GameObject.Find("WASD Keys");
		StartCoroutine(TutorialFlow ());
		welcomeText.SetActive (false);
		wasdKeysText.SetActive (false);
	}

	private IEnumerator TutorialFlow(){
		yield return new WaitForSeconds (startCountdown);
		welcomeText.SetActive ( true);
		yield return new WaitForSeconds (welcomeTextTime);
		welcomeText.SetActive (false);
		yield return new WaitForSeconds (Countdown1);
		wasdKeysText.SetActive (true);
		wasdKeysText.SetActive (false);
//		yield return new WaitWhile (!Input.GetKey ("w"));
//		yield return new WaitWhile (!Input.GetKey ("a"));
//		yield return new WaitWhile (!Input.GetKey ("s"));
//		yield return new WaitWhile (!Input.GetKey ("d"));

	}



	private void Update () {
//		//test
//		Time.timeScale = 0;
	}
}
