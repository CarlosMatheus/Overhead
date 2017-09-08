using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    [SerializeField] private GameObject fader = null;
    [SerializeField] private GameObject fadeCanvas = null;

    private float _currentValue;
    private AudioManager audioManager;
    private TutorialVerifier tutorialVerifier;
    private bool playGame = false;

    private void Start()
    {
        playGame = false;
        audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        tutorialVerifier = GameObject.FindWithTag("GameMaster").GetComponent<TutorialVerifier>();
    }

    public void LoadScene(int sceneNumber)
    {
        Time.timeScale = 1;
        if (sceneNumber == 3)
        {
            if ( SceneManager.GetActiveScene().buildIndex == 3 )
            {
                StartCoroutine(Fade(sceneNumber));
            }
            else if ( tutorialVerifier.GetPlayedTutorial() == true)
            {
                StartCoroutine(Fade(sceneNumber));
            }
            else if( playGame == false )
            {
                tutorialVerifier.AppearTutorialCanvas();
                playGame = true;
            }
            else
            {
                StartCoroutine(Fade(sceneNumber));
            }
        }
        else if (sceneNumber == 4)
        {
            tutorialVerifier.PlayTutorial();
            StartCoroutine(Fade(sceneNumber));
        }
        else
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
        if (CheckIfTheSceneIsMenu(sceneNumber))
            SceneManager.LoadScene(sceneNumber);
        else
            LoadManager.instance.CallLoadScene(sceneNumber);
    }

    bool CheckIfTheSceneIsMenu(int sceneNumber)
    {
        return sceneNumber == 2;
    }
}
