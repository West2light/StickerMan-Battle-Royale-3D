using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupHat : MonoBehaviour
{
    public BoxHat prefabBoxhat;
    public Transform content;

    public HatId selectingHatId;

    public List<BoxHat> hats = new List<BoxHat>();

    public Button btBuy;
    public Button btEquipped;
    public Text txPriceHat;

    public HatData currentHatData;
    private void Start()
    {
        CreateHats();
        btBuy.onClick.AddListener(CheckHatInfo);
    }

    private void CheckHightLight()
    {
        for (int i = 0; i < hats.Count; i++)
        {
            BoxHat hat = hats[i];
            hat.SetHighlight(hat.data.hatId == selectingHatId);
        }

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

        CheckHightLight();
        SetPrice();
    }

    public void OnHatSelected(HatId hatId)
    {
        selectingHatId = hatId;
        CheckHightLight();
        SetPrice();
    }

    public void SetPrice()
    {
        // từ selectingHatId => tìm đc HatData
        HatData selectingHatData = null;
        for (int i = 0; i < hats.Count; i++)
        {
            BoxHat hat = hats[i];
            if (hat.data.hatId == selectingHatId)
            {
                selectingHatData = hat.data;
                break;
            }
        }
        txPriceHat.text = selectingHatData.price.ToString();
    }
    public void CheckHatInfo()
    {
        currentHatData = null;
        BoxHat hat = null;
        for (int i = 0; i < hats.Count; i++)
        {
            hat = hats[i];
            if (hat.data.hatId == selectingHatId)
            {
                currentHatData = hat.data;
                break;
            }
        }
        bool isOwned = GameDataUser.IsOwnedHat(currentHatData.hatId);

        if (isOwned)
        {
            hat.imgLock.gameObject.SetActive(false);
            txPriceHat.gameObject.SetActive(false);
            btBuy.gameObject.SetActive(false);
        }
        else
        {
            hat.imgLock.gameObject.SetActive(true);
            txPriceHat.gameObject.SetActive(true);
            txPriceHat.text = currentHatData.price.ToString();
            btBuy.gameObject.SetActive(true);
            GameDataUser.BuyHat();
        }

    }

}
