using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabShield : MonoBehaviour
{
    public Button button;
    public Image imgBgShield;

    private void Awake()
    {
        button.onClick.AddListener(OnClick);
    }
    private void OnClick()
    {
        LobbyManager.Instance.popupSkin.Show(SkinTab.Shield);

    }
    private void Update()
    {
        Color color = imgBgShield.color;
        if (LobbyManager.Instance.popupSkin.currentTab == SkinTab.Shield)
        {
            color.a = 0f;
        }
        else
        {
            color.a = 225f;
        }
        imgBgShield.color = color;
    }
}
