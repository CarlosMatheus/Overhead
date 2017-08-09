using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScores : MonoBehaviour {

	private const string privateCode = "XntcOy_8-ECM3MrNdXoYFgMs2xVwo4Kke-YoP-zQojOw";
	private const string publicCode = "5980c12cb0b05c1ad4651d7a";
	private const string webURL = "http://dreamlo.com/lb/";

	private LeaderBoardControllerScript leaderBoardControllerScript;
    private PlayerDataCanvas[] playerDataCanvas;

	//public Highscore[] highscoreList;

	public void AddNewHighscore(string username, int score)
    {
		StartCoroutine (UploadNewHighScore (username, score));
	}

	public void DownloadHighscores()
    {
		StartCoroutine ("DownloadHighscoresFromDatabase");
	}

    public PlayerDataCanvas[] GetPlayerDataCanvas()
    {
        return playerDataCanvas;
    }
		
	private void Awake()
    {
		//AddNewHighscore("Carlos", 4564);
  //      AddNewHighscore("asdf", 546);
  //      AddNewHighscore("fdas", 786);
  //      AddNewHighscore("Carasdflos", 4575);
  //      AddNewHighscore("sdafsad", 76887);
  //      AddNewHighscore("asldfj", 34315);
  //      AddNewHighscore("asldfj", 454);
  //      AddNewHighscore("asldfj", 79);

  //      DownloadHighscores ();
	}

	private void Start()
    {
		leaderBoardControllerScript = GameObject.Find ("LeaderboardController").GetComponent<LeaderBoardControllerScript> ();
	}

	private void FormatHighscores(string textStream){
		string[] entries = textStream.Split (new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        //highscoreList = new Highscore[entries.Length];
        playerDataCanvas = new PlayerDataCanvas[entries.Length];

		for(int i = 0; i < entries.Length; i ++){
			string[] entryInfo = entries[i].Split (new char[] { '|' });

			string username = entryInfo[0];
			int score = int.Parse(entryInfo[1]);

            playerDataCanvas[i] = new PlayerDataCanvas(username,score, 47);

            //print (playerDataCanvas [i].username + ": " + highscoreList [i].score);

            print(playerDataCanvas[i].GetPlayerName() + ": " + playerDataCanvas[i].GetScore());
        }
	}

    IEnumerator DownloadHighscoresFromDatabase()
    {
        WWW www = new WWW(webURL + publicCode + "/pipe/");
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            FormatHighscores(www.text);
            leaderBoardControllerScript.OpenLeaderBoard();
        }
        else
        {
            print("Upload failed: " + www.error);
            leaderBoardControllerScript.ConnectionError(www.error);
        }

    }

    IEnumerator UploadNewHighScore(string username, int score)
    {
        WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
        yield return www;
        if (string.IsNullOrEmpty(www.error))
        {
            print("Upload Successful");
            DownloadHighscores();
        }
        else
        {
            print("Upload failed: " + www.error);
            leaderBoardControllerScript.ConnectionError(www.error);
        }
    }

    //public struct Highscore{
    //	public string username;
    //	public int score;

    //	//the structs constructor:
    //	public Highscore( string _username , int _score ){
    //		username = _username;
    //		score = _score;
    //	}
    //}

}
