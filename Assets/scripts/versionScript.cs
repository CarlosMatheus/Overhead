using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class versionScript : MonoBehaviour {

	public float fadeTime = 1.5f;
	public float fadeSpeed = 0.8f;
	public float startCountdown = 2f;
	public float welcomeTextTime = 5f;
	public float Countdown1 = 1f;
	public float Countdown2 = 1f;

	private bool fadeImage = false;
	private bool fadeVersionText = false;
	private bool fadeName1 = false;
	private bool fadeName2 = false;
	private bool fadeName3 = false;
	private int fadeDirImage = 1;
	private int fadeDirVersionText = 1;
	private int fadeDirName1 = 1;
	private int fadeDirName2 = 1;
	private int fadeDirName3 = 1;
	private GameObject image;
	private GameObject versionText;
	private GameObject name1;
	private GameObject name2;
	private GameObject name3;

	private void Start(){
		image = GameObject.Find ("Image");
		versionText = GameObject.Find ("VersionText");
		name1 = GameObject.Find ("Name1");
		name2 = GameObject.Find ("Name2");
		name3 = GameObject.Find ("Name3");
		StartCoroutine(VersionSceneFlow ());
		image.SetActive (false);
		versionText.SetActive (false);
		name1.SetActive (false);
		name2.SetActive (false);
		name3.SetActive (false);
		image.GetComponent<CanvasGroup> ().alpha = 0;
		versionText.GetComponent<CanvasGroup> ().alpha = 0;
		name1.GetComponent<CanvasGroup> ().alpha = 0;
		name2.GetComponent<CanvasGroup> ().alpha = 0;
		name3.GetComponent<CanvasGroup> ().alpha = 0;
	}

	private IEnumerator VersionSceneFlow(){
		
		yield return new WaitForSeconds (startCountdown);
		image.SetActive (true);
		fadeDirImage = 1;
		fadeImage = true;
		yield return new WaitForSeconds (fadeTime);

		versionText.SetActive (true);
		fadeDirVersionText = 1;
		fadeVersionText = true;
		yield return new WaitForSeconds (fadeTime);
		yield return new WaitForSeconds (fadeTime);
		fadeDirVersionText = -1;
		yield return new WaitForSeconds (fadeTime);

		name1.SetActive (true);
		fadeDirName1 = 1;
		fadeName1 = true;
		yield return new WaitForSeconds (fadeTime);
		name2.SetActive (true);
		fadeDirName2 = 1;
		fadeName2 = true;
		yield return new WaitForSeconds (fadeTime);
		fadeDirName1 = -1;
		name3.SetActive (true);
		fadeDirName3 = 1;
		fadeName3 = true;
		yield return new WaitForSeconds (fadeTime);
		fadeDirName2 = -1;
		yield return new WaitForSeconds (fadeTime);
		fadeDirName3 = -1;
		yield return new WaitForSeconds (fadeTime);

		fadeDirImage = -1;
		yield return new WaitForSeconds (fadeTime);
		image.SetActive (false);
		yield return new WaitForSeconds (startCountdown);
		SceneManager.LoadScene (0);
	}
		

	void Update(){
		if(fadeImage){
			image.GetComponent<CanvasGroup> ().alpha += fadeDirImage * fadeSpeed * Time.deltaTime; 
			image.GetComponent<CanvasGroup> ().alpha = Mathf.Clamp01 (image.GetComponent<CanvasGroup> ().alpha);
		}
		if(fadeVersionText){
			versionText.GetComponent<CanvasGroup> ().alpha += fadeDirVersionText * fadeSpeed * Time.deltaTime; 
			versionText.GetComponent<CanvasGroup> ().alpha = Mathf.Clamp01 (versionText.GetComponent<CanvasGroup> ().alpha);
		}
		if(fadeName1){
			name1.GetComponent<CanvasGroup> ().alpha += fadeDirName1 * fadeSpeed * Time.deltaTime; 
			name1.GetComponent<CanvasGroup> ().alpha = Mathf.Clamp01 (name1.GetComponent<CanvasGroup> ().alpha);
		}
		if(fadeName2){
			name2.GetComponent<CanvasGroup> ().alpha += fadeDirName2 * fadeSpeed * Time.deltaTime; 
			name2.GetComponent<CanvasGroup> ().alpha = Mathf.Clamp01 (name2.GetComponent<CanvasGroup> ().alpha);
		}
		if(fadeName3){
			name3.GetComponent<CanvasGroup> ().alpha += fadeDirName3 * fadeSpeed * Time.deltaTime; 
			name3.GetComponent<CanvasGroup> ().alpha = Mathf.Clamp01 (name3.GetComponent<CanvasGroup> ().alpha);
		}
	}
}
