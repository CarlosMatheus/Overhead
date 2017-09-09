using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour 
{
    [SerializeField] private float initialAnimationDuration = 5f;
    [SerializeField] private float interval = 20f;
    
    private WaveSpawner waveSpawner;
    private AudioManager audioManager;
    private CanvasManager canvasManager;
    private GameObject tutorial;
    private enum NextAction {Interval,Wave,Death,Tutorial};
    private NextAction nextAction;
    private bool isSpawning;
    private float countdown;
    private int numOfEnemies;

    public void SpawEnemy()
    {
        numOfEnemies++;
    }

    public void FinishSpawn()
    {
        isSpawning = false;
    }

    public void StartSpawn()
    {
        isSpawning = true;
    }

    public void KillEnemy()
    {
        numOfEnemies--;
        CheckEnemies();

        if (numOfEnemies < 0)
            Debug.LogError("There are a negative number of Enemies");
    }

    public void FinishTutorial()
    {
        tutorial.SetActive(false);
        countdown = -1f;
        canvasManager.SetTutorialNameCanvasAlpha(0);
        CurrentGameMode.SetGameMode(CurrentGameMode.GameMode.Normal);
    }

    private void CheckEnemies()
    {
        if (isSpawning == true) return;
        if (numOfEnemies == 0)
            countdown = - 1f;
        else
            return;
    }

    private void Start()
    {
        audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        canvasManager = gameObject.GetComponent<CanvasManager>();
        waveSpawner = gameObject.GetComponent<WaveSpawner>();
        tutorial = GameObject.Find("Tutorial");
        tutorial.SetActive(false);
        numOfEnemies = 0;

        StartInitialAnimation();
    }

    private void Update()
    {
        UpdateTime();
        waveSpawner.UpdateUI(countdown);
        CheckCountdown();
    }

    private void CheckCountdown()
    {
        if( countdown <= 0 )
        {
            DoNextAction();
        }
    }

    private void DoNextAction()
    {
        if( nextAction == NextAction.Interval )
        {
            StartInterval();
            return;
        }
        if (nextAction == NextAction.Wave)
        {
            StartWave();
            return;
        }
        if (nextAction == NextAction.Death)
        {
            StartDeath();
            return;
        }
        if (nextAction == NextAction.Tutorial)
        {
            StartTutorial();
            return;
        }
    }

    private void StartInitialAnimation()
    {
        countdown = initialAnimationDuration;
        audioManager.SetVolume("MusicMainScene", 0.6f);
        audioManager.PlayWithFade("MusicMainScene", 2f);
        canvasManager.SetCanvasAlpha(0);
        canvasManager.PlayInitialLoadingAnimation();
        canvasManager.PlayAppearCanvasWithDelay(4f);
        canvasManager.SetCanvasAfterAnimationWithDelay(4f);
        if(CurrentGameMode.IsInNormalMode())
            nextAction = NextAction.Interval;
        if (CurrentGameMode.IsInTutorialMode())
            nextAction = NextAction.Tutorial;
    }

    private void StartInterval()
    {
        countdown = interval;
        nextAction = NextAction.Wave;
        audioManager.Play("IntervalSound");
        audioManager.SetVolumeWithFade("MusicMainScene", 0.3f, 3f);
        canvasManager.PlayPrepareYourSelf();
        canvasManager.SetWaveCanvasAlpha(0);
        canvasManager.AppearWaveCoolDown();
    }

    private void StartWave()
    {
        //canvasManager.SetWaveCanvasAlpha(1f);
        canvasManager.AppearWaveCanvas();
        //canvasManager.SetWaveCoolDownAlpha(0);
        canvasManager.SetWaveCoolDownAlpha(0);
        audioManager.Play("NewWave");
        audioManager.SetVolumeWithFade("MusicMainScene", 0.6f, 3f);
        canvasManager.PlayWaveWarning();
        countdown = 10000f;
        waveSpawner.StartNextWave();
        nextAction = NextAction.Interval;
    }

    private void StartDeath()
    {
        
    }

    private void StartTutorial()
    {
        tutorial.SetActive(true);
        countdown = 10000f;
        canvasManager.SetWaveCanvasAlpha(0);
        canvasManager.SetWaveCoolDownAlpha(0);
        nextAction = NextAction.Interval;
        audioManager.SetVolumeWithFade("MusicMainScene", 0.3f, 3f);
        tutorial.transform.GetComponentInChildren<TutorialScript>().StartTutorial();
        canvasManager.AppearTutorialNameCanvas();
    }

    private void UpdateTime()
    {
        countdown = countdown - Time.deltaTime;
    }
}
