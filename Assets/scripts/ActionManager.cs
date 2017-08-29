﻿using System.Collections;
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
        audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        waveSpawner = gameObject.GetComponent<WaveSpawner>();
        canvasManager = gameObject.GetComponent<CanvasManager>();
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
        audioManager.PlayWithFade("MusicMainScene", 2f);
        nextAction = NextAction.Interval;
    }

    private void StartInterval()
    {
        countdown = interval;
        nextAction = NextAction.Wave;
        audioManager.Play("IntervalSound");
        audioManager.SetVolumeWithFade("MusicMainScene", 0.3f, 3f);
        canvasManager.PlayPrepareYourSelf();
    }

    private void StartWave()
    {
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