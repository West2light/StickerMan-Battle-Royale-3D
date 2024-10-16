using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxShield : MonoBehaviour
{
    public ShieldData shieldData;
    public Button button;
    public Image imgShield;
    public Image imgLock;
    public Image imgHightLight;


    public PopupShield popupShield;

    private void Awake()
    {
        button.onClick.AddListener(OnClick);
    }
    public void SetInfo(PopupShield popupShield, ShieldData data)
    {
        this.popupShield = popupShield;
        this.shieldData = data;
        this.imgShield.sprite = data.sprShield;

    }
    public void SetHightLight(bool isSet)
    {
        this.imgHightLight.gameObject.SetActive(isSet);
    }
    private void OnClick()
    {
        popupShield.OnShieldSelectd(shieldData.shieldId);
    }
}
