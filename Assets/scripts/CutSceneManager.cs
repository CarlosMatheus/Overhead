using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneManager : MonoBehaviour 
{
    public string nextSceneName;

    private void Start()
    {
        gameObject.GetComponent<MouseCursorManager>().SetInvisibleCursor();
    }

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
