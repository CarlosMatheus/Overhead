using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour {

	public float fadeTime = 1.5f;
	public float fadeSpeed = 0.8f;
	public float startCountdown = 2f;
	public float welcomeTextTime = 5f;
	public float Countdown1 = 1f;
	public float Countdown2 = 1f;

	private GameObject[] texts;
	private int fadeDir = 1;
	private int cont = 0; 
	private GameObject welcomeText;
	private GameObject wasdKeysText;
	private CanvasGroup canvasGroup;
	private bool fade;
	private GameObject wKey;
	private GameObject aKey;
	private GameObject sKey;
	private GameObject dKey;
	private GameObject flow;
	private float mainCountdown = 3.5f;

	private void Start(){
		flow = GameObject.Find ("Flow");
		//sets the texts
		texts = new GameObject[flow.transform.childCount];
		for(int i = 0; i < flow.transform.childCount; i ++){
			texts [i] = flow.transform.GetChild (i).gameObject;
		}
		wKey = GameObject.Find ("wKey");
		aKey = GameObject.Find ("aKey");
		sKey = GameObject.Find ("sKey");
		dKey = GameObject.Find ("dKey");
		canvasGroup = GameObject.Find ("TutorialCanvas").GetComponent<CanvasGroup> ();
		welcomeText = GameObject.Find("WelcomeText");
		wasdKeysText = GameObject.Find("WASD Keys");
		welcomeText.SetActive (false);
		wasdKeysText.SetActive (false);
		canvasGroup.alpha = 0;
		StartCoroutine(TutorialFlow ());
	}

	private IEnumerator TutorialFlow(){
		yield return new WaitForSeconds (startCountdown);
		welcomeText.SetActive (true);
		fadeDir = 1;
		fade = true;

		yield return new WaitForSeconds (welcomeTextTime);
		fadeDir = (-1);
		yield return new WaitForSeconds (fadeTime);
		welcomeText.SetActive (false);
		yield return new WaitForSeconds (Countdown1);
		wasdKeysText.SetActive (true);
		fadeDir = (1);
		yield return new WaitForSeconds (fadeTime);

		StartCoroutine(WaitForKeyDown ("w"));
		StartCoroutine(WaitForKeyDown ("a"));
		StartCoroutine(WaitForKeyDown ("s"));
		StartCoroutine(WaitForKeyDown ("d"));

		yield return new WaitUntil (() => cont >= 4);

		yield return new WaitForSeconds (Countdown2);
		fadeDir = (-1);
		yield return new WaitForSeconds (fadeTime);
		wasdKeysText.SetActive (false);

		for (int i = 0; i < texts.Length; i++) {
			texts [i].SetActive (true);
			fadeDir = (1);
			yield return new WaitForSeconds (fadeTime);
			yield return new WaitForSeconds (mainCountdown);
			fadeDir = (-1);
			yield return new WaitForSeconds (fadeTime);
			texts [i].SetActive (false);
		}

	}


	IEnumerator WaitForKeyDown(string key)
	{
		while (!Input.GetKey (key)) 
			yield return null;
		cont++;
	}
		
	void Update(){
		if(fade){
			canvasGroup.alpha += fadeDir * fadeSpeed * Time.deltaTime; 
			canvasGroup.alpha = Mathf.Clamp01 (canvasGroup.alpha);
		}
	}
}