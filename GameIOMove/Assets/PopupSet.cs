﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupSet : MonoBehaviour
{
    public BoxSet prefabSet;
    public Transform content;
    public Button btBuy;
    public Button btEquip;
    public Button btEquipped;
    public Text txGold;
    private List<BoxSet> sets = new List<BoxSet>();

    public SkinSetId selectingSetID;
    public SetData selectingSetData;

    private void OnEnable()
    {
        // Set selecting = SKIN đang equipped, nếu không có thì = NONE

        if (GameDataUser.equippedSkinSet != (int)SkinSetId.None)
        {
            selectingSetID = (SkinSetId)GameDataUser.equippedSkinSet;

        }
        else
        {
            selectingSetID = SkinSetId.None;
        }

        ReloadInfo();
    }
    private void Awake()
    {
        CreateSet();
        ReloadInfo();
        btBuy.onClick.AddListener(ClickButtonBuy);
        btEquip.onClick.AddListener(ClickOnButtonEquip);
    }
    private void CreateSet()
    {
        for (int i = 0; i < GameDataConstants.sets.Count; i++)
        {
            SetData data = GameDataConstants.sets[i];
            BoxSet instanceSet = Instantiate(prefabSet, content);
            instanceSet.SetInfo(this, data);
            sets.Add(instanceSet);
        }
        SetPrice();
        CheckHightLight();
    }
    private void ReloadInfo()
    {
        for (int i = 0; i < sets.Count; i++)
        {
            BoxSet set = sets[i];
            if (set.data.setId == selectingSetID)
            {
                selectingSetData = set.data;
                break;
            }
        }
        CheckHightLight();
        CheckButton();
        CheckUnlock();
        SetPrice();
    }
    private void CheckHightLight()
    {
        for (int i = 0; i < sets.Count; i++)
        {
            BoxSet set = sets[i];
            set.SetHightLight(selectingSetID == set.data.setId);
        }
    }
    public void OnSetSelected(SkinSetId setId)
    {
        selectingSetID = setId;
        LobbyManager.Instance.ChangePlayer((SkinSetId)GameDataUser.equippedSkinSet);
        ReloadInfo();
        CheckHightLight();
    }
    private void SetPrice()
    {
        if (selectingSetID != SkinSetId.None)
        {
            txGold.text = selectingSetData.price.ToString();
            if (GameDataUser.gold >= selectingSetData.price)
            {
                txGold.color = Color.black;
                btBuy.enabled = true;
            }
            else
            {
                txGold.color = Color.red;
                btBuy.enabled = false;
            }
            btBuy.gameObject.SetActive(true);
        }
        else
        {
            btBuy.gameObject.SetActive(false);
        }
    }
    private void ClickButtonBuy()
    {

        if (GameDataUser.ownedSkinSets.Contains((int)selectingSetData.setId))
        {
            return;
        }
        else
        {
            GameDataUser.gold -= selectingSetData.price;
            PlayerPrefs.SetInt(GameDataUser.PREF_KEY_GOLD, GameDataUser.gold);
            PlayerPrefs.Save();
            GameDataUser.BuySkinSet(selectingSetData.setId);
        }
        btBuy.gameObject.SetActive(false);
        btEquip.gameObject.SetActive(true);
        CheckUnlock();
    }
    private void CheckButton()
    {
        bool isOwnedSkinSet = GameDataUser.IsOwnedSkinSet(selectingSetID);
        bool isEquipped = (selectingSetID == (SkinSetId)GameDataUser.equippedSkinSet);
        if (GameDataUser.equippedSkinSet == (int)SkinSetId.None)
        {
            isEquipped = false;
        }
        if (isEquipped)
        {
            btBuy.gameObject.SetActive(false);
            btEquip.gameObject.SetActive(false);
            btEquipped.gameObject.SetActive(true);
            btEquipped.enabled = false;
        }
        else
        {
            if (isOwnedSkinSet)
            {
                btBuy.gameObject.SetActive(false);
                btEquip.gameObject.SetActive(true);
                btEquipped.gameObject.SetActive(false);
            }
            else
            {
                btBuy.gameObject.SetActive(true);
                btEquip.gameObject.SetActive(false);
                btEquipped.gameObject.SetActive(false);

            }
        }
    }
    private void CheckUnlock()
    {
        for (int j = 0; j < sets.Count; j++)
        {
            BoxSet set = sets[j];
            for (int i = 0; i < GameDataUser.ownedSkinSets.Count; i++)
            {
                if (GameDataUser.ownedSkinSets.Contains((int)set.data.setId))
                {
                    set.CheckLock(false);
                    break;
                }
            }
        }

    }
    private void ClickOnButtonEquip()
    {
        //GameDataUser.equippedHat = (int)HatId.None;
        //PlayerPrefs.SetInt(GameDataUser.PREF_KEY_EQUIPPED_HAT, GameDataUser.equippedHat);
        //PlayerPrefs.Save();
        //GameDataUser.equippedPant = (int)PantId.None;
        //PlayerPrefs.SetInt(GameDataUser.PREF_KEY_EQUIPPED_PANT, GameDataUser.equippedPant);
        //PlayerPrefs.Save();
        btEquip.gameObject.SetActive(false);
        btEquipped.gameObject.SetActive(true);
        btEquipped.enabled = false;

        GameDataUser.equippedSkinSet = (int)selectingSetID;
        PlayerPrefs.SetInt(GameDataUser.PREF_KEY_EQUIPPED_SKINSET, GameDataUser.equippedSkinSet);
        PlayerPrefs.Save();

        CheckUnlock();
    }


}


