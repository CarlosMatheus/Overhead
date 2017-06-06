using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour {

	public static ScoreCounter instance;
	public float InitialScore = 0f;
	public float InitialTowerScore = 1000f;
	public Text scoreText;

	private float score;
	private float scoreTower;

	public void SetScore(float value){
		score = value;
	}

	public float GetScore(){
		return score;
	}

	public void BuildTower(){
		score += scoreTower;
	}

	private void Start () {
		SetScore (InitialScore);
		scoreTower = InitialTowerScore;
		instance = this;
	}

	private void Update(){
		UpdateUI ();
	}

	private void UpdateUI(){
		scoreText.text = Mathf.Round(GetScore()).ToString();
	}
}
