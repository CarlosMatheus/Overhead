using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour 
{
    public static LoadManager instance;

    public int sceneToLoad { get; private set;}

    public void CallLoadScene (int buildIndex)
    {
        sceneToLoad = buildIndex;
        SceneManager.LoadScene("LoadingScreen");
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(this);
    }
}
