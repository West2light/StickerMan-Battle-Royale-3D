using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxSet : MonoBehaviour
{
    public Button button;
    public Image imgSet;
    public Image imgLock;
    public Image imgHightLight;

    public PopupSet popupSet;
    public SetData data;
    private void Awake()
    {
        button.onClick.AddListener(OnClick);
    }

    public void SetInfo(PopupSet popupSet, SetData data)
    {
        this.popupSet = popupSet;
        this.data = data;
        this.imgSet.sprite = data.sprSet;
    }
    public void SetHightLight(bool isOn)
    {
        this.imgHightLight.gameObject.SetActive(isOn);
    }
    private void OnClick()
    {
        popupSet.OnSetSelected(data.setId);
        LobbyManager.Instance.ChangePlayer(data.setId);
    }
    public void CheckLock(bool isUnLock)
    {
        imgLock.gameObject.SetActive(isUnLock);
    }
}
