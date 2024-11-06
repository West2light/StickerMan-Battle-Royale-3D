using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupPant : MonoBehaviour
{
    public BoxPant prefabPant;
    public Transform content;
    public Button btBuy;
    public Button btEquip;
    public Button btEquipped;
    public Text txPrice;
    private List<BoxPant> pants = new List<BoxPant>();

    private PantId selectingPantId;
    private PantData selectingPantData;
    private void Start()
    {
        CreatePant();
        btBuy.onClick.AddListener(ClickBtBuy);
    }

    private void CheckHightLight()
    {
        for (int i = 0; i < pants.Count; i++)
        {
            BoxPant pant = pants[i];
            pant.SetHightLight(selectingPantId == pant.data.id);
        }
    }
    private void CreatePant()
    {
        for (int i = 0; i < GameDataConstants.pants.Count; i++)
        {
            PantData pantData = GameDataConstants.pants[i];
            BoxPant instancePant = Instantiate(prefabPant, content);
            instancePant.SetInfo(this, pantData);
            pants.Add(instancePant);
        }
        ReloadInfo();
    }
    public void OnPantSelected(PantId pantId)
    {
        selectingPantId = pantId;
        //selectingPantData.id = selectingPantId;
        ReloadInfo();
    }
    private void ReloadInfo()
    {
        for (int i = 0; i < pants.Count; i++)
        {
            BoxPant pant = pants[i];
            if (pant.data.id == selectingPantId)
            {
                selectingPantData = pant.data;
                break;
            }
        }
        CheckButton();
        CheckUnLock();
        CheckHightLight();
        SetPrice();
    }
    private void ClickBtBuy()
    {
        if (GameDataUser.gold >= selectingPantData.price)
        {
            GameDataUser.gold -= selectingPantData.price;

            PlayerPrefs.SetInt(GameDataUser.PREF_KEY_GOLD, GameDataUser.gold);
            PlayerPrefs.Save();

            GameDataUser.BuyPant(selectingPantData.id);
        }
    }
    private void SetPrice()
    {
        if (selectingPantId != PantId.None)
        {
            txPrice.text = selectingPantData.price.ToString();
            if (GameDataUser.gold >= selectingPantData.price)
            {
                txPrice.color = Color.black;
                btBuy.enabled = true;
            }
            else
            {
                txPrice.color = Color.red;
                btBuy.enabled = false;
            }
            btBuy.gameObject.SetActive(true);
        }
        else
        {
            btBuy.gameObject.SetActive(false);
        }
    }
    private void CheckUnLock()
    {
        for (int j = 0; j < pants.Count; j++)
        {
            BoxPant pant = pants[j];
            for (int i = 0; i < GameDataUser.ownedPants.Count; i++)
            {
                if (pant.data.id == (PantId)GameDataUser.ownedPants[i])
                {
                    pant.CheckUnlock(false);
                    break;
                }
            }
        }

    }
    private void CheckButton()
    {
        bool isOwnedPant = GameDataUser.IsOwnedPant(selectingPantId);
        bool isEquipped = (selectingPantId == (PantId)GameDataUser.equippedPant);
        if (isEquipped)
        {
            btBuy.gameObject.SetActive(false);
            btEquip.gameObject.SetActive(false);
            btEquipped.gameObject.SetActive(true);
            btEquipped.enabled = false;
        }
        else
        {
            if (isOwnedPant)
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
