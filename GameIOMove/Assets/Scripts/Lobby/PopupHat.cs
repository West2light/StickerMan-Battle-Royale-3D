using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupHat : MonoBehaviour
{
    public BoxHat prefabBoxhat;
    public Transform content;
    public Button btBuy;
    public Button btEquip;
    public Button btEquipped;
    public Text txPriceHat;

    private HatId selectingHatId;
    private HatData selectingHatData;
    private List<BoxHat> hats = new List<BoxHat>();

    private void Awake()
    {
        CreateHats();
        btBuy.onClick.AddListener(ClickBtBuyHat);
    }
    private void Start()
    {
        btEquip.onClick.AddListener(ClickBtEquipp);
    }
    private void OnEnable()
    {
        // Set selecting = mũ đang equipped, nếu không có thì = NONE
        if (GameDataUser.equippedHat != (int)HatId.None)
        {
            selectingHatId = (HatId)GameDataUser.equippedHat;

        }
        else
        {
            selectingHatId = HatId.None;
        }

        ReloadInfo();
    }

    private void ReloadInfo()
    {
        for (int i = 0; i < hats.Count; i++)
        {
            BoxHat hat = hats[i];
            if (hat.data.hatId == selectingHatId)
            {
                selectingHatData = hat.data;
                break;
            }
        }

        CheckHightLight();
        CheckButtons();
        CheckUnLock();
        SetPrice();
    }

    private void CreateHats()
    {
        for (int i = 0; i < GameDataConstants.hats.Count; i++)
        {
            HatData hatData = GameDataConstants.hats[i];

            BoxHat instanceBoxHat = Instantiate(prefabBoxhat, content);
            instanceBoxHat.SetInfo(this, hatData);
            hats.Add(instanceBoxHat);
        }
    }

    public void OnHatSelected(HatId hatId)
    {
        LobbyManager.Instance.popupSkin.ReloadDefaultSkin();
        selectingHatId = hatId;
        ReloadInfo();
    }

    private void CheckHightLight()
    {
        for (int i = 0; i < hats.Count; i++)
        {
            BoxHat hat = hats[i];
            hat.SetHighlight(hat.data.hatId == selectingHatId);
        }
    }
    private void CheckUnLock()
    {
        for (int j = 0; j < hats.Count; j++)
        {
            BoxHat hat = hats[j];

            // Kiểm tra xem mũ hiện tại có nằm trong danh sách sở hữu không
            for (int i = 0; i < GameDataUser.ownedHats.Count; i++)
            {
                if (hat.data.hatId == (HatId)GameDataUser.ownedHats[i]) // So sánh ID của mũ
                {
                    hat.CheckLock(false);
                    break;
                }
            }


        }
    }
    private void ClickBtEquipp()
    {
        //if (selectingHatData.hatId != (HatId)GameDataUser.equippedHat)
        //{
        GameDataUser.equippedSkinSet = (int)SkinSetId.None;
        PlayerPrefs.SetInt(GameDataUser.PREF_KEY_EQUIPPED_SKINSET, GameDataUser.equippedSkinSet);
        PlayerPrefs.Save();

        GameDataUser.equippedHat = (int)selectingHatData.hatId;
        PlayerPrefs.SetInt(GameDataUser.PREF_KEY_EQUIPPED_HAT, GameDataUser.equippedHat);
        PlayerPrefs.Save();

        btEquip.gameObject.SetActive(false);
        btEquipped.gameObject.SetActive(true);
        btEquipped.enabled = false;
        ReloadInfo();
        //}
    }

    private void SetPrice()
    {
        if (selectingHatId != HatId.None)
        {
            txPriceHat.text = selectingHatData.price.ToString();

            if (GameDataUser.gold >= selectingHatData.price)
            {
                txPriceHat.color = Color.black;
                btBuy.enabled = true;
            }
            else
            {
                txPriceHat.color = Color.red;
                btBuy.enabled = false;
            }

            btBuy.gameObject.SetActive(true);
        }
        else
        {
            btBuy.gameObject.SetActive(false);
        }
    }

    private void CheckButtons()
    {
        // check hiển thị các nút
        bool isOwned = GameDataUser.IsOwnedHat(selectingHatId);
        bool isEquipped = (selectingHatId == (HatId)GameDataUser.equippedHat);
        if (GameDataUser.equippedSkinSet != (int)SkinSetId.None)
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
            if (isOwned)
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
        //for (int i = 0; i < GameDataUser.ownedHats.Count; i++)
        //{
        //    if (selectingHatId == (HatId)GameDataUser.ownedHats[i])
        //    {
        //        isOwend = true;
        //        break;
        //    }

        //}
        //if (isOwend)
        //{
        //    btBuy.gameObject.SetActive(false);
        //    btEquip.gameObject.SetActive(true);
        //}
        //else
        //{
        //    btBuy.gameObject.SetActive(true);
        //    btEquip.gameObject.SetActive(false);

        //}

    }

    public void ClickBtBuyHat()
    {
        if (GameDataUser.gold >= selectingHatData.price)
        {
            GameDataUser.gold -= selectingHatData.price;
            PlayerPrefs.SetInt(GameDataUser.PREF_KEY_GOLD, GameDataUser.gold);
            PlayerPrefs.Save();
            GameDataUser.BuyHat(selectingHatId);
            ReloadInfo();
        }
    }
}
