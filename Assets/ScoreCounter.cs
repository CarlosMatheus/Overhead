using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour {

	public float InitialScore = 0;
	public Text scoreText;

	private float score;

	public void SetScore(float value){
		score = value;
	}

	public float GetScore(){
		return score;
	}

	private void Start () {
		SetScore (InitialScore);
	}

	private void Update(){
		UpdateUI ();
	}

	private void UpdateUI(){
		scoreText.text = Mathf.Round(GetScore()).ToString();
	}
}
