using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour 
{
    [SerializeField] private float initialAnimationDuration = 5f;
    [SerializeField] private float interval = 20f;
    
    private WaveSpawner waveSpawner;
    private AudioManager audioManager;
    private enum NextAction {Interval,Wave,Death};
    private NextAction nextAction;
    private float countdown;
    private int numOfEnemies;

    public void SpawEnemy()
    {
        numOfEnemies++;
    }

    public void KillEnemy()
    {
        numOfEnemies--;
        CheckEnemies();
    }

    private void CheckEnemies()
    {
        if (numOfEnemies == 0)
            countdown = -1f;
        else
            return;
    }

    private void Start()
    {
        audioManager = gameObject.GetComponent<AudioManager>();
        waveSpawner = gameObject.GetComponent<WaveSpawner>();
        StartInitialAnimation();
        numOfEnemies = 0;
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
        audioManager.Play("StartMainScene");
        audioManager.PlayWithFade("MusicMainScene", 3f);
        nextAction = NextAction.Interval;
    }

    private void StartInterval()
    {
        countdown = interval;
        nextAction = NextAction.Wave;
        audioManager.SetVolumeWithFade("MusicMainScene", 0.3f, 3f);
    }

    private void StartWave()
    {
        audioManager.Play("NewWave");
        audioManager.SetVolumeWithFade("MusicMainScene", 0.6f, 3f);
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
