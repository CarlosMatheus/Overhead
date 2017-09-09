using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour 
{
    [SerializeField] private GameObject fader = null;
    [SerializeField] private GameObject fadeCanvas = null;

    private TutorialVerifier tutorialVerifier;
    private AudioManager audioManager;
    private float _currentValue;
    private bool playGame;

    public void LoadNormalMainScene()
    {
        string sceneName = "Main";
        Time.timeScale = 1;
        CurrentGameMode.SetGameMode(CurrentGameMode.GameMode.Normal);
        if (tutorialVerifier.GetPlayedTutorial() == true)
        {
            StartCoroutine(Fade(sceneName));
        }
        else if (playGame == false)
        {
            tutorialVerifier.AppearTutorialCanvas();
            playGame = true;
        }
        else
        {
            StartCoroutine(Fade(sceneName));
        }
    }

    public void LoadTutorialMainScene()
    {
        string sceneName = "Main";
        Time.timeScale = 1;
        CurrentGameMode.SetGameMode(CurrentGameMode.GameMode.Tutorial);
        tutorialVerifier.PlayTutorial();
        StartCoroutine(Fade(sceneName));
    }

    public void LoadMainMenuScene()
    {
        string sceneName = "MainMenu";
        Time.timeScale = 1;
        StartCoroutine(Fade(sceneName));
    }

    public void RestartScene()
    {
        Time.timeScale = 1;
        StartCoroutine(Fade(SceneManager.GetActiveScene().name));
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void Start()
    {
        playGame = false;
        audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        tutorialVerifier = GameObject.FindWithTag("GameMaster").GetComponent<TutorialVerifier>();
    }

    private IEnumerator Fade(string sceneName)
    {
        fader.SetActive(true);
        fadeCanvas.SetActive(true);
        while (fadeCanvas.GetComponent<CanvasGroup>().alpha < 0.99)
        {
            _currentValue = fadeCanvas.GetComponent<CanvasGroup>().alpha + 0.9f * Time.deltaTime;
            fadeCanvas.GetComponent<CanvasGroup>().alpha = _currentValue;
            yield return null;
        }
        if (CheckIfTheSceneIsMenu(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            LoadManager.instance.CallLoadScene(SceneManager.GetSceneByName(sceneName).buildIndex);
        }
    }

    private bool CheckIfTheSceneIsMenu(string sceneName)
    {
        return sceneName == "MainMenu";
    }
}
