using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    [SerializeField] private GameObject Canvas;

    private float _currentValue;
    private GameObject fadeCanvas;

    private void Start()
    {
        fadeCanvas = Canvas.transform.Find("FadeCanvas").gameObject;
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

    IEnumerator Fade(int sceneNumber)
    {
        Canvas.SetActive(true);
        fadeCanvas.SetActive(true);
        while (fadeCanvas.GetComponent<CanvasGroup>().alpha < 0.99)
        {
            _currentValue = fadeCanvas.GetComponent<CanvasGroup>().alpha + 0.9f * Time.deltaTime;
            fadeCanvas.GetComponent<CanvasGroup>().alpha = _currentValue;
            yield return null;
        }
        SceneManager.LoadScene(sceneNumber);
    }
}
