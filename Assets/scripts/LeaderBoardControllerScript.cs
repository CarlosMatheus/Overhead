using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardControllerScript : MonoBehaviour {

	private HighScores highScores;
	private DeathManager deathManager;
	private Fading fading;
	private bool cancel = false; 
	private float wave;
	private float score;
	private string name;


	public void GetInput(string _name){
		name = _name;
		Debug.Log (name);
		UploadHighscore();
	}

	public void Cancel(){
		cancel = true;
		fading.DisappearPlayerScoreCanvas ();
		fading.AppearOfflineScoreCanvas ();
	}

	public void ConnectionError(string ErrorMessage){
		Debug.Log ("Upload failed: " + ErrorMessage);
		GameObject.Find("ErrorMessage").gameObject.GetComponent<Text>().text = ("Error message: " + ErrorMessage);
	}

	public void UploadHighscore(){
		highScores.AddNewHighscore (name, (int)score);
	}

	public void SetWave(float _wave){
		wave = _wave;
	}

	public void SetScore(float _score){
		score = _score;
	}

	private void Start(){
		highScores = GameObject.Find ("GameMaster").GetComponent<HighScores> ();
		deathManager = GameObject.Find ("GameMaster").GetComponent<DeathManager> ();
		fading = GameObject.Find ("GameMaster").GetComponent<Fading> ();
	}
}
