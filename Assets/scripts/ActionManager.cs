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
    private enum NextAction {Interval,Wave,Death};
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
        waveSpawner = gameObject.GetComponent<WaveSpawner>();
        canvasManager = gameObject.GetComponent<CanvasManager>();
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
    }

    private void StartInitialAnimation()
    {
        countdown = initialAnimationDuration;
        audioManager.PlayWithFade("MusicMainScene", 2f);
        nextAction = NextAction.Interval;
        canvasManager.SetCanvasAlpha(0);
        canvasManager.PlayInitialLoadingAnimation();
        canvasManager.PlayAppearCanvasWithDelay(4f);
        canvasManager.SetCanvasAfterAnimationWithDelay(4f);
    }

    private void StartInterval()
    {
        countdown = interval;
        canvasManager.SetWaveCanvasAlpha(0);
        canvasManager.SetWaveCoolDownAlpha(1f);
        nextAction = NextAction.Wave;
        audioManager.Play("IntervalSound");
        audioManager.SetVolumeWithFade("MusicMainScene", 0.3f, 3f);
        canvasManager.PlayPrepareYourSelf();
    }

    private void StartWave()
    {
        canvasManager.SetWaveCanvasAlpha(1f);
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

    private void UpdateTime()
    {
        countdown = countdown - Time.deltaTime;
    }
}
