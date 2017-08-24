using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour {

    [SerializeField] private GameObject fader;
    [SerializeField] private GameObject fadeCanvas;

    private float _currentValue;

    // Use this for initialization
    void Start () {
        StartCoroutine(LoadScene());
	}
	
	IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(3);

        //Load Scene in the background
        AsyncOperation async = SceneManager.LoadSceneAsync(LoadManager.instance.sceneToLoad, LoadSceneMode.Single);
        async.allowSceneActivation = false;
        while (async.progress < 0.9f)
            yield return null;

        //Checking if it went through all the array
        while (!GetComponent<TextGenerator>().isReady)
            yield return null;

        //Fading
        fader.SetActive(true);
        fadeCanvas.SetActive(true);
        while (fadeCanvas.GetComponent<CanvasGroup>().alpha < 0.99)
        {
            _currentValue = fadeCanvas.GetComponent<CanvasGroup>().alpha + 0.9f * Time.deltaTime;
            fadeCanvas.GetComponent<CanvasGroup>().alpha = _currentValue;
            yield return null;
        }

        //Actually change the scene
        async.allowSceneActivation = true;
    }
}
