﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardControllerScript : MonoBehaviour {

    [SerializeField] GameObject playerScoreCanvas;
    [SerializeField] GameObject connectionErrorCanvas;
    [SerializeField] GameObject offlineScoreCanvas;
    [SerializeField] GameObject leaderBoadCanvas;
    [SerializeField] GameObject leaderBoadCanceledCanvas;
    [SerializeField] GameObject list;
    [SerializeField] GameObject listC;

    private PlayerDataCanvas[] playerDataCanvas;
    private GameObject invalidInput;
    private GameObject nameList;
    private GameObject waveList;
    private GameObject scoreList;
    private GameObject nameListC;
    private GameObject waveListC;
    private GameObject scoreListC;
    private HighScores highScores;
	private DeathManager deathManager;
	private Fading fading;
	private bool cancel = false;
    private bool isPlayerInList = false;
    private bool isFirstTime = true;
	private float wave;
	private float score;
    private int playerRank;
	private string playerName;

    public void SetPlayerScoreCanvasActive(bool setActive)
    {
        playerScoreCanvas.SetActive(setActive);
        invalidInput = GameObject.Find("InvalidInput");
        invalidInput.SetActive(false);
    }

    public void SetConnectionErrorCanvas(bool setActive)
    {
        connectionErrorCanvas.SetActive(setActive);
    }

    public void SetOfflineScoreCanvas(bool setActive)
    {
        offlineScoreCanvas.SetActive(setActive);
    }

    public void SetleaderBoadCanvas(bool setActive)
    {
        leaderBoadCanvas.SetActive(setActive);
    }

    public void SetleaderBoadCanceledCanvas(bool setActive)
    {
        leaderBoadCanceledCanvas.SetActive(setActive);
    }

    public void GetInput(string _name){
        if (IsNameAllowed(_name) == true)
            UploadHighscore(_name);
        else
            invalidInput.SetActive(true);
	}

	public void CancelWithNoError(){
		cancel = true;
		fading.DisappearPlayerScoreCanvas ();
		fading.AppearLeaderBoadCanceledCanvas ();
	}

	public void CancelDueToError(){
		cancel = true;
		fading.DisappearConnectionErrorMessageCanvas ();
		fading.AppearOfflineScoreCanvas ();
	}

	public void ConnectionError(string ErrorMessage){
		Debug.Log ("Upload failed: " + ErrorMessage);
		GameObject.Find("ErrorMessage").gameObject.GetComponent<Text>().text = ("Error message: " + ErrorMessage);
	}

	public void UploadHighscore(string _name){
        if (isFirstTime == true)
        {
            playerName = _name;
            isFirstTime = false;
            //FilterName();
            //Debug.Log(playerName);
            //Debug.Log((int)score);
            fading.DisappearPlayerScoreCanvas();
            highScores.AddNewHighscore(playerName, (int)score);
        }
	}

    public bool IsNameAllowed(string _name)
    {
        for(int i = 0; i < _name.Length; i++)
        {
            if (char.IsLetterOrDigit(_name[i]) == false)
                return false;
        }
        return true;
    }

    //public void FilterName()
    //{
    //    print("name of the player before filter: " + playerName);
    //    //playerName = playerName.Trim( new char[] { '|','.' } );
    //    playerName = playerName.Replace('|', '0');
    //    playerName = playerName.Replace(' ', '0');
    //    print("name of the player filted: " + playerName);
    //}

	public void OpenLeaderBoard()
    {
        if(cancel == false)
        {
            fading.AppearLeaderBoardCanvas();
            SetHighScoreBoard();
        }
        else
        {
            fading.AppearLeaderBoadCanceledCanvas();
            SetHighScoreBoardCancelled();
        }

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

        SetleaderBoadCanvas(true);
        SetOfflineScoreCanvas(true);
        nameList = GameObject.Find("NameList");
        waveList = GameObject.Find("WaveList");
        scoreList = GameObject.Find("ScoreList");
        nameListC = GameObject.Find("NameListC");
        waveListC = GameObject.Find("WaveListC");
        scoreListC = GameObject.Find("ScoreListC");
        SetleaderBoadCanvas(false);
        SetOfflineScoreCanvas(false);
    }

    private void SetHighScoreBoard()
    {
        playerDataCanvas = highScores.GetPlayerDataCanvas();

        for (int i = 0; i < 7; i ++)
        {
            nameList.transform.GetChild(i).gameObject.GetComponent<Text>().text = playerDataCanvas[i].GetPlayerName();
            scoreList.transform.GetChild(i).gameObject.GetComponent<Text>().text = playerDataCanvas[i].GetScore();
            waveList.transform.GetChild(i).gameObject.GetComponent<Text>().text = playerDataCanvas[i].GetWave();
        }

        FindPlayerInList();
        PlayerHighScore();
        AjustPlayerBoard();
        
    }

    private void SetHighScoreBoardCancelled()
    {
        PlayerDataCanvas[] playerDataCanvas = highScores.GetPlayerDataCanvas();



    }

    private void AjustPlayerBoard()
    {
        if (isPlayerInList == true)
        {
            GameObject.Find("RankPlayer").GetComponent<Text>().text = playerRank.ToString();
            wave = float.Parse(playerDataCanvas[playerRank - 1].GetWave());
            score = float.Parse(playerDataCanvas[playerRank - 1].GetScore());
        }
        else
        {
            GameObject.Find("RankPlayer").GetComponent<Text>().text = ">1000.";
        }

        GameObject.Find("WavePlayer").GetComponent<Text>().text = wave.ToString();
        GameObject.Find("ScorePlayer").GetComponent<Text>().text = score.ToString();
        GameObject.Find("NamePlayer").GetComponent<Text>().text = playerName;
        GameObject.Find("MassagePlayer").GetComponent<Text>().text = (playerName + ", your corrent hightest score is:");
    }

    private void FindPlayerInList()
    {
        int i;
        for (i = 0; i < playerDataCanvas.Length ; i++)
        {
            if (string.Equals(playerDataCanvas[i].GetPlayerName(), playerName) == true)
            {
                Debug.Log("Encontrou");
                isPlayerInList = true;
                playerRank = i+1;
                break;
            }
        }
    }

    private void PlayerHighScore()
    {
        if (playerRank < 8)
        {
            Debug.Log("Passou 1");
            float gap = 40f;

            GameObject highScoreBadge = GameObject.Find("HighScoreBadge"+playerRank.ToString());
            highScoreBadge.GetComponent<CanvasGroup>().alpha = 1f;
            GameObject highScoreBadges = GameObject.Find("HighScoreBadges");
            for (int i= 0; i < 7; i ++)
                highScoreBadges.transform.GetChild(i).gameObject.SetActive(false);
            highScoreBadge.SetActive(true);
    
        }
        else
        {
            Debug.Log("Passou 2");
            GameObject highScoreBadges = GameObject.Find("HighScoreBadges");
            highScoreBadges.SetActive(false);
        }
    }

}
