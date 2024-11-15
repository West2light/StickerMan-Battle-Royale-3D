using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SkinTab
{

    Hat = 0,
    Pant = 1,
    Shield = 2,
    SkinSet = 3,
}

public class PopupSkin : MonoBehaviour
{
    public List<GameObject> popups;
    public SkinTab currentTab;
    public Button btExit;
    public Player player;
    private void Awake()
    {
        Show(SkinTab.Pant);
        btExit.onClick.AddListener(DeactivePopupSkin);
    }

    public void Show(SkinTab tab)
    {
        currentTab = tab;

        for (int i = 0; i < popups.Count; i++)
        {
            popups[i].SetActive(i == (int)currentTab);
        }
        if (currentTab == SkinTab.SkinSet)
        {
            LobbyManager.Instance.ChangePlayer((SkinSetId)GameDataUser.equippedSkinSet);
        }
    }
    public void DeactivePopupSkin()
    {
        gameObject.SetActive(false);

        if (GameDataUser.equippedSkinSet != (int)SkinSetId.None)
        {
            this.player.gameObject.SetActive(false);
            LobbyManager.Instance.ChangePlayer((SkinSetId)GameDataUser.equippedSkinSet);
        }
        else
        {
            GameDataUser.equippedSkinSet = (int)SkinSetId.None;
            ReloadDefaultSkin();
        }

    }
    public void ReloadDefaultSkin()
    {
        LobbyManager.Instance.player.gameObject.SetActive(false);
        LobbyManager.Instance.player = this.player;
        LobbyManager.Instance.player.gameObject.SetActive(true);
    }

}
