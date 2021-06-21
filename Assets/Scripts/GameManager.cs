using System.Collections;
using System.Collections.Generic;
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
}
