using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxHat : MonoBehaviour
{
    public Button button;
    public Image imgHat;
    public Image imgLock;
    public Image imgHightLight;

    public HatData data;

    private PopupHat popupHat;

    private void Awake()
    {
        button.onClick.AddListener(OnClick);
    }

    public void SetInfo(PopupHat popupHat, HatData data)
    {
        this.popupHat = popupHat;
        this.data = data;
        imgHat.sprite = data.imageHat;
    }

    public void SetHighlight(bool isOn)
    {
        imgHightLight.gameObject.SetActive(isOn);
    }

    private void OnClick()
    {
        popupHat.OnHatSelected(data.hatId);
        LobbyManager.Instance.player.EquipHat(data.hatId);
    }


}
