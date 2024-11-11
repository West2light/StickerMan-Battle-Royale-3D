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
        if (currentTab != SkinTab.SkinSet)
        {
            player.ReloadDefaultOutfit();
        }
        for (int i = 0; i < popups.Count; i++)
        {
            popups[i].SetActive(i == (int)currentTab);
        }
        if (popups[(int)SkinTab.SkinSet].activeSelf == false)
        {
            LobbyManager.Instance.player.gameObject.SetActive(false);
            this.player.gameObject.SetActive(true);
            LobbyManager.Instance.player = player;
        }
        else
        {
            return;
        }
    }
    public void DeactivePopupSkin()
    {
        gameObject.SetActive(false);
        if (GameDataUser.equippedSkinSet == (int)SkinSetId.None)
        {
            LobbyManager.Instance.player.gameObject.SetActive(false);
            this.player.gameObject.SetActive(true);
            LobbyManager.Instance.player = player;
        }
        else
        {
            LobbyManager.Instance.player.ReloadDefaultOutfit();
        }
    }

}
