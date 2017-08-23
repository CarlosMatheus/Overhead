using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour 
{
    [SerializeField] private float initialAnimationDuration = 5f;
    [SerializeField] private float interval = 20f;
    
    private WaveSpawner waveSpawner;
    private GameObject[] ObjArray;
    private string nextAction;
    private float countdown;
    private float time;

    public void CheckEnemies()
    {
        ObjArray = GameObject.FindGameObjectsWithTag("Enemy");
        if (ObjArray.Length == 0)
            countdown = -1f;
        else
            return;
    }

    private void Start()
    {
        time = 0;
        countdown = initialAnimationDuration;
        waveSpawner = gameObject.GetComponent<WaveSpawner>();
        nextAction = "Interval";
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
        if( nextAction == "Interval" )
        {
            
            return;
        }
        if (nextAction == "Wave")
        {
            StartWave();
            return;
        }
        if (nextAction == "Death")
        {
            
            return;
        }
    }

    private void StartInitialAnimation()
    {

    }

    private void StartInterval()
    {
        countdown = interval;
        nextAction = "Wave";
    }

    private void StartWave()
    {
        countdown = 10000f;
        waveSpawner.StartNextWave();
        nextAction = "Interval";
    }

    private void StartDeath()
    {
        
    }

    private void UpdateTime()
    {
        time = time + Time.deltaTime;
        countdown = countdown - Time.deltaTime;
    }

}
