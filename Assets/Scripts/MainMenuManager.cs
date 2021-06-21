using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameField;
    [SerializeField] private Button newGameButton;
    [SerializeField] private GameObject highScorePanel;
    [SerializeField] private TextMeshProUGUI highScoreTable;
    [SerializeField] private GameObject mainMenuPanel;


    // Start is called before the first frame update
    void Start()
    {
        nameField.text = GameManager.Instance.playerName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        //MainManager.Instance.SaveColor();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }


    public void NameChanged()
    {
        if (nameField.text.Length == 3)
        {
            newGameButton.interactable = true;
            GameManager.Instance.playerName = nameField.text;
        }
        else
        {
            newGameButton.interactable = false;
            GameManager.Instance.playerName = null;
        }
    }


    public void ShowHighScorePanel()
    {
        highScoreTable.text = GenerateHighScoreTable();
        highScorePanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void HideHighScorePanel()
    {
        highScorePanel.SetActive(false);
        mainMenuPanel.SetActive(true);

    }

    private string GenerateHighScoreTable()
    {
        string highScoreText = "";
        if (GameManager.Instance.highScore.Count == 0) return highScoreText;

        int index = 1;
        foreach (var player in GameManager.Instance.highScore)
        {
            highScoreText += (index < 10 ? " " : "") + index + ". " + player.name.PadRight(13 - (int)Math.Floor(Math.Log10(player.score) + 1),'.') + player.score +"\n";
            index++;
        }
        return highScoreText;
    }

}
