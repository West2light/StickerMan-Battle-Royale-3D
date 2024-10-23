using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxPant : MonoBehaviour
{
    public Button button;
    public Image imgPant;
    public Image imgLock;
    public Image imgHightLight;

    public PantData data;
    private PopupPant popupPant;

    private void Awake()
    {
        button.onClick.AddListener(OnClick);
    }
    public void SetInfo(PopupPant popupPant, PantData data)
    {
        this.popupPant = popupPant;
        this.data = data;
        this.imgPant.sprite = data.imagePant;
    }
    public void SetHightLight(bool isSet)
    {
        this.imgHightLight.gameObject.SetActive(isSet);
    }
    private void OnClick()
    {
        popupPant.OnPantSelected(data.id);
        LobbyManager.Instance.player.EquipPant(data.id);
    }
}
