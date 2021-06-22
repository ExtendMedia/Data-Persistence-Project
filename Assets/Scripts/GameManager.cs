using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditorInternal;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public string playerName;
    public List<Player> highScore = new List<Player>();
    
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);

    }


    public void AddScore(string playerName, int playerScore)
    {
        if (highScore.Count>0)
        {
            highScore.Add(new Player(playerName, playerScore));
            highScore.Sort((p1, p2) => p2.score.CompareTo(p1.score));
            if (highScore.Count > 10) highScore.RemoveAt(highScore.Count - 1);
        }
        else
        {
            highScore.Add(new Player(playerName, playerScore));
        }
    }



    public void SaveHighScore()
    {
        HighScore wrapper = new HighScore();
        wrapper.highScoreList = highScore;
        string json = JsonUtility.ToJson(wrapper);

        File.WriteAllText(Application.persistentDataPath + "/highscore.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/highscore.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            HighScore wrapper = JsonUtility.FromJson<HighScore>(json);
            if (wrapper.highScoreList != null) highScore = wrapper.highScoreList;

        }
    }
}
