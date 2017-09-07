using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulsCounter : MonoBehaviour {

    public static SoulsCounter instance;

	[SerializeField] private string[] killersTags;
	[SerializeField] private float[] baseKillValues;
    [SerializeField] private float initialSouls = 30f;
    [SerializeField] private float initialScoreConst = 3f;
    [SerializeField] private float waveKillConst;

    private float scoreConstant;
	private float souls;
	private float wave;
	private Text soulsText;
	private ScoreCounter scoreCounter;
    private ActionManager actionManager;
    private WaveSpawner waveSpawner;
	private float[] towerValue;

	public void SetInitialTowersValues(float[] towerV)
    {
		towerValue = towerV;
	}

	public void SetWave(float value)
    {
		wave = value;
	}

	public float GetWave()
    {

		return wave*2;
	}

	public void SetSouls(float value)
    {
		souls = value;
	}

	public float GetSouls ()
    {
		return souls;
	}

	public void SetTowerPrice (float value, int index)
    {
		towerValue[index] = value;
	}

	public float GetTowerPrice (int index)
    {
		return towerValue[index];
	}

	public void BuildTower (int index)
    {
		souls -= towerValue[index];
	}

	public bool CanBuild (int towerIndex)
    {
		if ( towerValue[towerIndex] <= souls )
			return true;
		else
			return false;
	}

	public void KillEnemy (float value)
    {
		souls += value;
		scoreCounter.KillEnemy( ConvertToScore (value) );
	}

	public void AddSouls (string _tag)
    {
		float value = KillerPrice (_tag);
		souls += value;
	}

	public void AddSouls (float _value) {
		souls += _value;
	}

	private float ConvertToScore(float value)
    {
		return value * scoreConstant * wave;
	}

	private void Start ()
    {
		SetSouls (initialSouls);
        scoreConstant = initialScoreConst;
		instance = this;
		scoreCounter = this.GetComponent<ScoreCounter> ();
		soulsText = GameObject.Find ("SoulsNum").GetComponent<Text> ();
        waveSpawner = gameObject.GetComponent<WaveSpawner>();
        actionManager = gameObject.GetComponent<ActionManager>();
	}

	private void Update ()
    {
		UpdateUI ();
	}

	private void UpdateUI ()
    {
		soulsText.text = Mathf.Round(GetSouls()).ToString();
	}

    private float GetKillingValue(int i)
    {
        float val = Mathf.Round( (baseKillValues[i] * (Mathf.Pow(waveKillConst, waveSpawner.GetWave()))) );

        return val;
    }

	public float KillerPrice (string _toSearch)
    {
		for (int i = 0; i < killersTags.Length; i++)
			if (killersTags [i] == _toSearch)
                return GetKillingValue(i);
        return GetKillingValue (0);
	}
}