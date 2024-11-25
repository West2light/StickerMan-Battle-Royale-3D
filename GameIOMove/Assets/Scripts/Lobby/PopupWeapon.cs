using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class PopupWeapon : MonoBehaviour
{


    public Image imgWeapon;
    public Text txWeaponRange;
    public Text txWeaponDamage;
    public Button btSellect;
    public Button btEquipped;
    public Button btClose;
    public Text txSellect;
    public Text txNameWp;
    public Text txPrice;

    public Button leftButton;
    public Button rightButton;
    public Button btBuy;


    private int currentIndex = -1;

    public WeaponData wpSelectingData;

    private void Awake()
    {
        currentIndex = 0;
        btClose.onClick.AddListener(ClickBtClose);
        btBuy.onClick.AddListener(ClickBtBuyWp);
    }
    private void Start()
    {
        ShowWeaponDetails(currentIndex);
        leftButton.onClick.AddListener(ClickBtLeft);
        rightButton.onClick.AddListener(ClickBtRight);
        btSellect.onClick.AddListener(ClickBtSellect);
    }

    private void ClickBtClose()
    {
        gameObject.SetActive(false);
        if (GameDataUser.equippedSkinSet != (int)SkinSetId.None)
        {
            LobbyManager.Instance.ChangePlayer((SkinSetId)GameDataUser.equippedSkinSet);
        }
        else
        {
            LobbyManager.Instance.player.ReloadDefaultOutfit();
        }
    }

    private WeaponData currentWeapon;

    private void ClickBtLeft()
    {
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = GameDataConstants.weapons.Count - 1;
        }
        ShowWeaponDetails(currentIndex);
    }
    private void ClickBtRight()
    {
        currentIndex++;
        if (currentIndex > GameDataConstants.weapons.Count - 1)
        {
            currentIndex = 0;
        }
        ShowWeaponDetails(currentIndex);
    }

    private void ClickBtSellect()
    {
        if (wpSelectingData.id != (WeaponId)GameDataUser.equippedWeapon)
        {

            GameDataUser.equippedWeapon = (int)wpSelectingData.id;

            PlayerPrefs.SetInt(GameDataUser.PREF_KEY_EQUIPPED_WEAPON, GameDataUser.equippedWeapon);
            PlayerPrefs.Save();

            btSellect.gameObject.SetActive(false);
            btEquipped.gameObject.SetActive(true);
            btEquipped.enabled = false;
        }

    }



    private void ClickBtBuyWp()
    {
        if (GameDataUser.gold >= wpSelectingData.price)
        {
            GameDataUser.gold -= wpSelectingData.price;
            PlayerPrefs.SetInt(GameDataUser.PREF_KEY_GOLD, GameDataUser.gold);
            PlayerPrefs.Save();
            GameDataUser.BuyWeapon(wpSelectingData.id);

            //Change button buy -> select
            btBuy.gameObject.SetActive(false);
            btSellect.gameObject.SetActive(true);
            CheckButton();
        }
    }

    // This method will be called to show the weapon details on the popup
    private void ShowWeaponDetails(int index)
    {

        for (int i = 0; i < GameDataConstants.weapons.Count; i++)
        {
            WeaponData weapon = GameDataConstants.weapons[i];

            if (i == index)
            {
                currentIndex = i;
                imgWeapon.sprite = weapon.icon;
                txWeaponDamage.text = string.Format("Damage:{0}", weapon.damage.ToString());
                txWeaponRange.text = string.Format("Range:{0}", weapon.range.ToString());
                txNameWp.text = weapon.weaponName;
                if (GameDataUser.gold >= weapon.price)
                {
                    txPrice.color = Color.black;
                }
                else
                {
                    txPrice.color = Color.red;
                }
                txPrice.text = weapon.price.ToString();
                wpSelectingData = weapon;
                LobbyManager.Instance.player.EquipWeapon(weapon.id);
            }
        }
        CheckButton();

    }
    private void CheckButton()
    {
        bool isOwend = false;
        for (int i = 0; i < GameDataUser.ownedWeapons.Count; i++)
        {
            if (wpSelectingData.id == (WeaponId)GameDataUser.ownedWeapons[i])
            {
                isOwend = true;
                break;
            }
        }

        if (wpSelectingData.id == (WeaponId)GameDataUser.equippedWeapon)
        {
            btBuy.gameObject.SetActive(false);
            btSellect.gameObject.SetActive(false);
            btEquipped.gameObject.SetActive(true);
            btEquipped.enabled = false;
        }
        else
        {
            if (isOwend)
            {
                btBuy.gameObject.SetActive(false);
                btSellect.gameObject.SetActive(true);
                btEquipped.gameObject.SetActive(false);
            }
            else
            {
                btBuy.gameObject.SetActive(true);
                btSellect.gameObject.SetActive(false);
                btEquipped.gameObject.SetActive(false);
            }
        }
    }
}

