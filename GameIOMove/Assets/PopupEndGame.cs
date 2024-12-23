using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopupEndGame : MonoBehaviour
{
    public Button btExit;
    public Text txEndGame;
    private void Awake()
    {
        btExit.onClick.AddListener(OnClickExit);
        EditText();
    }
    private void OnClickExit()
    {
        SceneManager.LoadScene("Lobby");
    }
    private void EditText()
    {
        txEndGame.color = Color.red;
    }

}
