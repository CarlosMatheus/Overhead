using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour 
{

    [SerializeField] private float initialAnimationDuration;
    [SerializeField] private float interval;
    [SerializeField] private float waveTime;
    
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
    }

    private void Update()
    {
        UpdateTime();
        waveSpawner.UpdateUI();
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
        if (nextAction == "Interval")
        {


            return;
        }
        if (nextAction == "Interval")
        {


            return;
        }
    }

    private void UpdateTime()
    {
        time = time + Time.deltaTime;
        countdown = countdown - Time.deltaTime;
    }

    private void InitialAnimation()
    {
        
    }



    IEnumerator InitialAnimationCorrout()
    {

        yield return WaitForSeconds( i );

    }


}
