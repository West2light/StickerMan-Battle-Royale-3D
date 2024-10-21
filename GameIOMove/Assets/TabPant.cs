using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabPant : MonoBehaviour
{
    public Button btPant;
    public Image imgBgPant;

    private void Awake()
    {
        btPant.onClick.AddListener(OnClick);
    }
    private void OnClick()
    {
        LobbyManager.Instance.popupSkin.Show(SkinTab.Pant);
    }
    private void Update()
    {
        Color color = imgBgPant.color;
        if (LobbyManager.Instance.popupSkin.currentTab == SkinTab.Pant)
        {
            color.a = 0f;
        }
        else
        {
            color.a = 225f;
        }
        imgBgPant.color = color;
    }
}
