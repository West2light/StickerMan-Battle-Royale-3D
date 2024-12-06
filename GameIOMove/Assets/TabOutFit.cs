using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabOutFit : MonoBehaviour
{
    public Button btOutFit;
    public Image imgBgOutFit;
    private void Awake()
    {
        btOutFit.onClick.AddListener(OnClick);
    }
    private void OnClick()
    {
        LobbyManager.Instance.popupSkin.Show(SkinTab.SkinSet);

    }
    private void Update()
    {
        Color color = imgBgOutFit.color;
        if (LobbyManager.Instance.popupSkin.currentTab == SkinTab.SkinSet)
        {
            color.a = 0f;
        }
        else
        {
            color.a = 225f;
        }
        imgBgOutFit.color = color;
    }
}
