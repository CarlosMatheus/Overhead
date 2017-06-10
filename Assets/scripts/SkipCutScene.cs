using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipCutScene : MonoBehaviour {

    public string nextSceneName;

    private void Update()
    {
        if (Input.anyKey)
            LoadNewScene();
    }


    public void LoadNewScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
