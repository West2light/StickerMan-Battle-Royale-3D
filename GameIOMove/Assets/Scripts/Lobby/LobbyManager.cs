using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : Singleton<LobbyManager>
{
    public Player player;
    public Button btPlay;
    public Button btSkin;
    public Button btWeapon;
    public PopupWeapon popupWeapon;
    public PopupSkin popupSkin;
    public Text txGold;

    private void Awake()
    {
        GameDataConstants.Load();
        GameDataUser.Load();

        btPlay.onClick.AddListener(ClickBtPlay);
        btSkin.onClick.AddListener(ClickBtSkin);
        btWeapon.onClick.AddListener(ClickBtWeapon);
    }

    private void Update()
    {
        txGold.text = GameDataUser.gold.ToString();
        if (Input.GetKeyUp(KeyCode.G))
        {
            Debug.Log("goldHave=" + GameDataUser.gold);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            int goldAdd = Random.Range(100, 1000);
            GameDataUser.AddGold(goldAdd);
            Debug.LogFormat("add={0}, goldHave={1}", goldAdd, GameDataUser.gold);
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            int goldConsume = Random.Range(100, 1000);
            GameDataUser.ConsumeGold(goldConsume);
            Debug.LogFormat("consume={0}, goldHave={1}", goldConsume, GameDataUser.gold);
        }
    }

    private void ClickBtPlay()
    {

    }

    private void ClickBtSkin()
    {
        popupSkin.gameObject.SetActive(true);
    }

    private void ClickBtWeapon()
    {
        popupWeapon.gameObject.SetActive(true);
    }
}
