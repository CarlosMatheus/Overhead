using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulsCounter : MonoBehaviour {

	//Make it easier to instantiate:
	public static SoulsCounter instance;
	public float InitialSouls = 200f;
	public float scoreConstant = 200f;
	public Text soulsText;
	public string[] killersTags;
	public float[] killValues;

	private float souls;
	private float towerPrice;
	private float wave;
	private ScoreCounter scoreCounter;

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

	public void SetTowerPrice(float value){
		towerPrice = value;
	}

	public float GetTowerPrice(){
		return towerPrice;
	}

	public void BuildTower(){
		souls -= towerPrice;
	}

	public bool CanBuild(){
		if (towerPrice <= souls)
			return true;
		else
			return false;
	}

	public void KillEnemy (string _tag) {
		float value = KillerPrice (_tag);
		souls += value;
		scoreCounter.SetScore (scoreCounter.GetScore () + ConvertToScore (value));
	}

	private float ConvertToScore(float value){
		return value * scoreConstant * wave;
	}

	private void Start () {
		SetSouls (InitialSouls);
		towerPrice = 10f;
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