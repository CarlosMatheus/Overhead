using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulsCounter : MonoBehaviour {

	//Make it easier to instantiate:
	public static SoulsCounter instance;
	public float InitialSouls = 60f;
	public float scoreConstant = 2f;
	public Text soulsText;
	public string[] killersTags;
	public float[] killValues;

	private float souls;
	private float wave;
	private ScoreCounter scoreCounter;
	private float[] towerValue;

	public void setInitialTowersValues(float[] towerV){
		towerValue = towerV;
	}

	public void SetWave(float value){
		wave = value;
	}

	public float GetWave(){
		return wave;
	}

	public void SetSouls(float value){
		souls = value;
	}

	public float GetSouls(){
		return souls;
	}

	public void SetTowerPrice(float value, int index){
		towerValue[index] = value;
	}

	public float GetTowerPrice(int index){
		return towerValue[index];
	}

	public void BuildTower( int index){
		souls -= towerValue[index];
	}

	public bool CanBuild(int towerIndex){
		if (towerValue[towerIndex] <= souls)
			return true;
		else
			return false;
	}

	public void KillEnemy (string _tag) {
		float value = KillerPrice (_tag);
		souls += value;
		scoreCounter.KillEnemy(ConvertToScore (value));
	}

	private float ConvertToScore(float value){
		return value * scoreConstant * wave;
	}

	private void Start () {
		SetSouls (InitialSouls);
		instance = this;
		scoreCounter = this.GetComponent<ScoreCounter> ();
	}

	private void Update () {
		UpdateUI ();
	}

	private void UpdateUI () {
		soulsText.text = Mathf.Round(GetSouls()).ToString();
	}

	private float KillerPrice (string _toSearch) {
		for (int i = 0; i < killersTags.Length; i++)
			if (killersTags [i] == _toSearch)
				return killValues[i];
		return killValues [0];
	}
}