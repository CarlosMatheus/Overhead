using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathManager : MonoBehaviour {

    [SerializeField] private GameObject LifeManager;
    [SerializeField] private GameObject MainCamera;
    [SerializeField] private GameObject DeathCamera;
    [SerializeField] private GameObject Canvas;
    [SerializeField] private GameObject GameOverCanvas;

    private float numberOfLives = 10;
    private float score = 0;
    private float wave = 0;
    private bool currentState = false;
    private bool previousState = false;
    private bool lost = false;
    private GameObject _currentUI;
	private bool isDead = false;
	
	void Start () {
		
	}

	public bool IsDead(){
		return isDead;
	}
	
	void Update () {
        previousState = currentState;

        numberOfLives = LifeManager.GetComponent<MasterTowerScript>().GetLifes();

        if (numberOfLives > 0)
            currentState = false;
        else
            currentState = true;

        if (previousState != currentState)
        {
            lost = true;
            LoseTheGame();
        }
	}

    void LoseTheGame ()
    {
		isDead = true;
		LifeManager.GetComponent<MasterTowerScript>().enabled = false;
		score = GetComponent<ScoreCounter>().GetScore();
		GetComponent<ScoreCounter>().enabled = false;
		GetComponent<SoulsCounter>().enabled = false;
        wave = GetComponent<WaveSpawner>().GetWave() - 1;
        DeathCamera.SetActive(true);
        MainCamera.SetActive(false);
        MainCamera.transform.Find("OutlineCamera").gameObject.SetActive(false);
        StartCoroutine(FadeCanvas());
        //Disabling stuff
        GetComponent<BuildManager>().enabled = false;
        GetComponent<ExtraFunctionalities>().enabled = false;
        MainCamera.transform.parent.gameObject.GetComponent<CameraControllerScript>().enabled = false;
        GameObject.Find("Bruxo").GetComponent<PlayerController>().enabled = false;
        StartCoroutine(Blur());
        StartCoroutine(AppearCanvas());
    }

    IEnumerator Blur()
    {
        DeathCamera.GetComponent<Blur>().Resolution = 1;

        for (int i = 1; i <= 10; i++)
        {
            DeathCamera.GetComponent<Blur>().NumberOfIterations = i;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator FadeCanvas()
    {
        for (int i = 1; i <= 7; i ++)
        {
            //Canvas.GetComponent<CanvasGroup>().alpha = Canvas.GetComponent<CanvasGroup>().alpha/2;
            Canvas.GetComponent<CanvasGroup>().alpha = Canvas.GetComponent<CanvasGroup>().alpha - 1/7f;
            yield return new WaitForSeconds(0.05f);
        }
        Canvas.SetActive(false);
    }

    IEnumerator AppearCanvas()
    {
        yield return new WaitForSeconds(0.5f);

        GameOverCanvas.SetActive(true);

        _currentUI = GameOverCanvas.transform.Find("FinalScoreText").gameObject;
        for (int i = 1; i <= 8; i++)
        {
            _currentUI.GetComponent<CanvasGroup>().alpha = _currentUI.GetComponent<CanvasGroup>().alpha + 1 / 8f;
            yield return new WaitForSeconds(0.07f);
        }

        _currentUI = GameOverCanvas.transform.Find("WaveNumberText").gameObject;
        for (int i = 1; i <= 8; i++)
        {
            _currentUI.GetComponent<CanvasGroup>().alpha = _currentUI.GetComponent<CanvasGroup>().alpha + 1 / 8f;
            yield return new WaitForSeconds(0.07f);
        }

        _currentUI = GameOverCanvas.transform.Find("Score").gameObject;
        _currentUI.GetComponent<Text>().text = score.ToString();
        for (int i = 1; i <= 4; i++)
        {
            _currentUI.GetComponent<CanvasGroup>().alpha = _currentUI.GetComponent<CanvasGroup>().alpha + 1 / 4f;
            yield return new WaitForSeconds(0.05f);
        }

        _currentUI = GameOverCanvas.transform.Find("Waves").gameObject;
        _currentUI.GetComponent<Text>().text = wave.ToString();
        for (int i = 1; i <= 4; i++)
        {
            _currentUI.GetComponent<CanvasGroup>().alpha = _currentUI.GetComponent<CanvasGroup>().alpha + 1 / 4f;
            yield return new WaitForSeconds(0.05f);
        }

        _currentUI = GameOverCanvas.transform.Find("PlayAgain").gameObject;
        for (int i = 1; i <= 5; i++)
        {
            GameOverCanvas.transform.Find("PlayAgain").gameObject.GetComponent<CanvasGroup>().alpha = GameOverCanvas.transform.Find("PlayAgain").gameObject.GetComponent<CanvasGroup>().alpha + 1 / 5f;
            GameOverCanvas.transform.Find("MainMenu").gameObject.GetComponent<CanvasGroup>().alpha = GameOverCanvas.transform.Find("MainMenu").gameObject.GetComponent<CanvasGroup>().alpha + 1 / 5f;
            yield return new WaitForSeconds(0.07f);
        }
    }
}
