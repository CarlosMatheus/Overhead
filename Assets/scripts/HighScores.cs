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
		
	}

	private void Start()
    {
		leaderBoardControllerScript = GameObject.Find ("LeaderboardController").GetComponent<LeaderBoardControllerScript> ();
	}

	private void FormatHighscores(string textStream){
		string[] entries = textStream.Split (new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        playerDataCanvas = new PlayerDataCanvas[entries.Length];

		for(int i = 0; i < entries.Length; i ++){
			string[] entryInfo = entries[i].Split (new char[] { '|' });

			string username = entryInfo[0];
			int score = int.Parse(entryInfo[1]);

            playerDataCanvas[i] = new PlayerDataCanvas(username,score, 47);
            
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

        print(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);

        if (string.IsNullOrEmpty(www.error))
        {
            //print("Fez upload de " + username + " com sucesso!");
            //print("Upload Successful");
            DownloadHighscores();
        }
        else
        {
            print("Upload failed: " + www.error);
            leaderBoardControllerScript.ConnectionError(www.error);
        }
    }
}
