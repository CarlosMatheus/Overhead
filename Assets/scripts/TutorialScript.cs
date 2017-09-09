using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialScript : MonoBehaviour 
{
	[SerializeField] private float welcomeTextTime = 5f;
    [SerializeField] private float startCountdown = 2f;
	[SerializeField] private float Countdown1 = 1f;
	[SerializeField] private float Countdown2 = 1f;
    [SerializeField] private float fadeSpeed = 0.8f;
    [SerializeField] private float fadeTime = 1.5f;

	private int fadeDir = 1;
	private int cont = 0; 
    private bool fade;
    private float mainCountdown = 5f;
	private GameObject welcomeText;
	private GameObject wasdKeysText;
	private GameObject wKey;
	private GameObject aKey;
	private GameObject sKey;
	private GameObject dKey;
	private GameObject flow;
    private GameObject[] texts;
    private CanvasGroup canvasGroup;
    private ActionManager actionManager;

    public void StartTutorial()
    {
        StartCoroutine(TutorialFlow());
    }

	private void Start()
    {
        actionManager = GameObject.FindWithTag("GameMaster").GetComponent<ActionManager>();
		flow = GameObject.Find ("Flow");
		//sets the texts
		texts = new GameObject[flow.transform.childCount];
		for(int i = 0; i < flow.transform.childCount; i ++)
        {
			texts [i] = flow.transform.GetChild (i).gameObject;
			texts [i].SetActive (false);
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
	}

	private IEnumerator TutorialFlow()
    {
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

		yield return new WaitUntil ( ( ) => cont >= 4 );

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

		yield return new WaitForSeconds (mainCountdown);

        actionManager.FinishTutorial();
	}

	IEnumerator WaitForKeyDown(string key)
	{
		while (!Input.GetKey (key)) 
			yield return null;
		cont++;
	}
	
	void Update()
    {
		if( fade )
        {
			canvasGroup.alpha += fadeDir * fadeSpeed * Time.deltaTime; 
			canvasGroup.alpha = Mathf.Clamp01 (canvasGroup.alpha);
		}
	}
}