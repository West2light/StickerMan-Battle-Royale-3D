using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabHat : MonoBehaviour
{
    public Button btHat;
    public Image imgBgHat;

    private void Awake()
    {
        btHat.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        LobbyManager.Instance.popupSkin.Show(SkinTab.Hat);
    }

    private void Update()
    {
        Color color = imgBgHat.color;
        if (LobbyManager.Instance.popupSkin.currentTab == SkinTab.Hat)
        {
            color.a = 0f;
        }
        else
        {
            color.a = 225f;
        }
        imgBgHat.color = color;
    }
}
