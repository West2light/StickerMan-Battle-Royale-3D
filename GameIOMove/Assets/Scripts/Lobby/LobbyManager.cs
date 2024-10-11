using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : Singleton<LobbyManager>
{
    public Button btPlay;
    public Button btSkin;
    public Button btWeapon;
    public PopupWeapon popupWeapon;

    private void Awake()
    {
        GameDataConstants.Load();
        btPlay.onClick.AddListener(ClickBtPlay);
        btSkin.onClick.AddListener(ClickBtSkin);
        btWeapon.onClick.AddListener(ClickBtWeapon);
    }

    private void ClickBtPlay()
    {

    }

    private void ClickBtSkin()
    {

    }

    private void ClickBtWeapon()
    {
        popupWeapon.gameObject.SetActive(true);
    }
}
