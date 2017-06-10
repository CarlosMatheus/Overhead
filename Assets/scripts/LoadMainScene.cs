using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainScene : MonoBehaviour {

    public void LoadNewScene()
    {
        SceneManager.LoadScene("version");
    }

    private void Start()
    {
        LoadNewScene();
    }
}
