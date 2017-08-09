using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataCanvas : MonoBehaviour {

    private string playerName;
    private int score;
    private int wave;

    public PlayerDataCanvas(string _playerName, int _score, int _wave)
    {
        playerName = _playerName;
        score = _score;
        wave = _wave;
    }

    public string GetPlayerName()
    {
        return playerName;
    }

    public string GetScore()
    {
        return score.ToString();
    }

    public string GetWave()
    {
        return wave.ToString();
    }

}
