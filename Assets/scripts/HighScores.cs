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

	public void AddNewHighscore(string username, int score, int wave)
    {
		StartCoroutine (UploadNewHighScore (username, score, wave));
	}

	public void DownloadHighscores()
    {
		StartCoroutine ("DownloadHighscoresFromDatabase");
	}

    public PlayerDataCanvas[] GetPlayerDataCanvas()
    {
        return playerDataCanvas;
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
            int wave = int.Parse(entryInfo[2]);

            playerDataCanvas[i] = new PlayerDataCanvas(username,score, wave);
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
            leaderBoardControllerScript.ConnectionError(www.error);
        }

    }

    IEnumerator UploadNewHighScore(string username, int score, int wave)
    {
        WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score + "/" + wave);
        yield return www;

        print(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);

        if (string.IsNullOrEmpty(www.error))
        {
            DownloadHighscores();
        }
        else
        {
            leaderBoardControllerScript.ConnectionError(www.error);
        }
    }
}
