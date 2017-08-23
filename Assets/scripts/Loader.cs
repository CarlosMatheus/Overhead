using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(LoadScene());
	}
	
	IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(2);

        AsyncOperation async = SceneManager.LoadSceneAsync(LoadManager.instance.sceneToLoad, LoadSceneMode.Single);
        async.allowSceneActivation = false;
        while (async.progress < 0.9f)
            yield return null;



        async.allowSceneActivation = true;
    }
}
