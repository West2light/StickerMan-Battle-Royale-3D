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
        if (popups[(int)SkinTab.SkinSet].activeSelf == false)
        {
            for (int j = 1; j <= LobbyManager.Instance.playerMap.Count; j++)
            {
                if ((SkinSetId)j != SkinSetId.None)
                {
                    LobbyManager.Instance.playerMap[(SkinSetId)j].gameObject.SetActive(false);
                }
            }
            LobbyManager.Instance.player = this.player;
            LobbyManager.Instance.player.gameObject.SetActive(true);
            LobbyManager.Instance.player.ReloadDefaultOutfit();
            return;
        }
        else
        {
            LobbyManager.Instance.ChangePlayer((SkinSetId)GameDataUser.equippedSkinSet);
            return;
        }
        //if (popups[(int)SkinTab.SkinSet].activeSelf == false)
        //{
        //    LobbyManager.Instance.player.gameObject.SetActive(false);
        //    this.player.gameObject.SetActive(true);
        //    LobbyManager.Instance.player = player;
        //}
        //else
        //{
        //    if (GameDataUser.equippedSkinSet != (int)SkinSetId.None) 
        //    {
        //        this.player.gameObject.SetActive(false);
        //        LobbyManager.Instance.player
        //    }
        //    return;
        //}
    }
    public void DeactivePopupSkin()
    {
        gameObject.SetActive(false);

        if (GameDataUser.equippedSkinSet != (int)SkinSetId.None)
        {
            if (popups[(int)SkinTab.SkinSet].activeSelf == false)
            {
                for (int j = 1; j <= LobbyManager.Instance.playerMap.Count; j++)
                {
                    if ((SkinSetId)j != SkinSetId.None)
                    {
                        LobbyManager.Instance.playerMap[(SkinSetId)j].gameObject.SetActive(false);
                    }
                }
                LobbyManager.Instance.player = this.player;
                LobbyManager.Instance.player.gameObject.SetActive(true);
                LobbyManager.Instance.player.ReloadDefaultOutfit();
            }
            else
            {
                this.player.gameObject.SetActive(false);
                LobbyManager.Instance.ChangePlayer((SkinSetId)GameDataUser.equippedSkinSet);
            }
        }
        else
        {
            for (int j = 1; j <= LobbyManager.Instance.playerMap.Count; j++)
            {
                if ((SkinSetId)j != SkinSetId.None)
                {
                    LobbyManager.Instance.playerMap[(SkinSetId)j].gameObject.SetActive(false);
                }
            }


            LobbyManager.Instance.player.gameObject.SetActive(true);
            LobbyManager.Instance.player = this.player;
            LobbyManager.Instance.player.ReloadDefaultOutfit();
        }

    }


}
