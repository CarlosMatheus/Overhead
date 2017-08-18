using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    [SerializeField] private GameObject fader;
    [SerializeField] private GameObject fadeCanvas;

    private float _currentValue;
    //private GameObject fadeCanvas;

    private void Start()
    {
    }

    public void LoadScene(int sceneNumber)
    {
        Time.timeScale = 1;
        StartCoroutine(Fade(sceneNumber));
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public IEnumerator Fade(int sceneNumber)
    {
        fader.SetActive(true);
        fadeCanvas.SetActive(true);
        while (fadeCanvas.GetComponent<CanvasGroup>().alpha < 0.99)
        {
            _currentValue = fadeCanvas.GetComponent<CanvasGroup>().alpha + 0.9f * Time.deltaTime;
            fadeCanvas.GetComponent<CanvasGroup>().alpha = _currentValue;
            yield return null;
        }
        SceneManager.LoadScene(sceneNumber);
        //LoadManager.instance.CallLoadScene(sceneNumber);
    }
}
