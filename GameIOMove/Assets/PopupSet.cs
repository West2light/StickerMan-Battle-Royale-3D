using System.Collections;
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
        // Set selecting = mũ đang equipped, nếu không có thì = NONE

        if (GameDataUser.equippedSkinSet != null)
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
        btBuy.onClick.AddListener(ClickButtonBuy);
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
        if (GameDataUser.ownedSkinSet.Contains((int)selectingSetData.setId))
        {
            return;
        }
        else
        {
            GameDataUser.gold -= selectingSetData.price;
            PlayerPrefs.SetInt(GameDataUser.PREF_KEY_GOLD, GameDataUser.gold);
            PlayerPrefs.Save();
            GameDataUser.ownedSkinSet.Add((int)selectingSetData.setId);
        }

    }
    private void CheckButton()
    {
        bool isOwnedSkinSet = GameDataUser.IsOwnedSkinSet(selectingSetID);
        bool isEquipped = (selectingSetID == (SkinSetId)GameDataUser.equippedSkinSet);
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
}
