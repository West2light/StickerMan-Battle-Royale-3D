using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupHat : MonoBehaviour
{
    public BoxHat prefabBoxhat;
    public Transform content;
    public Button btBuy;
    public Button btEquipped;
    public Text txPriceHat;

    private HatId selectingHatId;
    private HatData selectingHatData;
    private List<BoxHat> hats = new List<BoxHat>();
    private bool isUnLock;


    private void Awake()
    {
        isUnLock = true;
        CreateHats();
        btBuy.onClick.AddListener(ClickBtBuyHat);
    }

    private void OnEnable()
    {
        // Set selecting = mũ đang equipped, nếu không có thì = NONE

        if (GameDataUser.equippedHat != null)
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
            isUnLock = true; // Mặc định là khóa cho mỗi mũ

            // Kiểm tra xem mũ hiện tại có nằm trong danh sách sở hữu không
            for (int i = 0; i < GameDataUser.owenedHats.Count; i++)
            {
                if (hat.data.hatId == (HatId)GameDataUser.owenedHats[i]) // So sánh ID của mũ
                {
                    isUnLock = false;
                    break;
                }
            }

            // Gọi CheckLock với trạng thái của mũ (mở khóa hoặc khóa)
            hat.CheckLock(isUnLock);
        }
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
        bool isOwernd = false;

        for (int i = 0; i < GameDataUser.owenedHats.Count; i++)
        {
            if (selectingHatId == (HatId)GameDataUser.owenedHats[i])
            {
                isOwernd = true;
                break;
            }

        }
        if (isOwernd)
        {
            btBuy.gameObject.SetActive(false);
            btEquipped.gameObject.SetActive(true);
        }
        else
        {
            btBuy.gameObject.SetActive(true);
            btEquipped.gameObject.SetActive(false);
        }

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

        //    currentHatData = null;
        //    BoxHat selectedHat = null;
        //    for (int i = 0; i < hats.Count; i++)
        //    {
        //        BoxHat hat = hats[i];
        //        if (hat.data.hatId == selectingHatId)
        //        {
        //            selectedHat = hat;
        //            currentHatData = hat.data;
        //            break;
        //        }
        //    }
        //    bool isOwned = GameDataUser.IsOwnedHat(currentHatData.hatId);

        //    if (isOwned)
        //    {
        //        if (selectedHat != null)
        //        {
        //            selectedHat.imgLock.gameObject.SetActive(false);
        //        }
        //        btBuy.gameObject.SetActive(false);
        //    }
        //    else
        //    {
        //        if (selectedHat != null)
        //        {
        //            selectedHat.imgLock.gameObject.SetActive(true);
        //        }

        //        txPriceHat.gameObject.SetActive(true);
        //        txPriceHat.text = currentHatData.price.ToString();
        //        btBuy.gameObject.SetActive(true);
        //        GameDataUser.BuyHat();
        //    }

        //    for (int i = 0; i < GameDataUser.owernedHats.Count; i++)
        //    {
        //        HatData hatOwerned = new HatData();
        //        hatOwerned.hatId = GameDataUser.owernedHats[i];
        //        currentHatData = hatOwerned;
        //    }

        //}

    }
}
