using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Button btExit;
    public TMP_Text txGameOver;

    private void Awake()
    {
        EditText();
        btExit.onClick.AddListener(ClickOnbtExit);
    }

    private void ClickOnbtExit()
    {
        SceneManager.LoadScene("Lobby");
    }
    private void EditText()
    {
        string text = "<color=#FF0000>Game</color> <color=#0000FF>Over</color>";
        txGameOver.text = text;
    }
}
