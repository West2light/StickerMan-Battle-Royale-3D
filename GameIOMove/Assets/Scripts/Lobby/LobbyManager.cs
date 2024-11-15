using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : Singleton<LobbyManager>
{
    public Player player;
    public Button btPlay;
    public Button btSkin;
    public Button btWeapon;
    public PopupWeapon popupWeapon;
    public PopupSkin popupSkin;
    public Text txGold;

    public List<Player> playerSkins = new List<Player>();
    public Dictionary<SkinSetId, Player> playerMap;

    private void OnEnable()
    {
        playerMap = new Dictionary<SkinSetId, Player>
        {
            {SkinSetId.Angel, playerSkins[0]},
            {SkinSetId.DeadPool, playerSkins[1]},
            {SkinSetId.Devil, playerSkins[2]},
            {SkinSetId.Thor, playerSkins[3]},
            {SkinSetId.Witch, playerSkins[4]},
        };
        if (GameDataUser.equippedSkinSet != (int)SkinSetId.None)
        {
            ChangePlayer((SkinSetId)GameDataUser.equippedSkinSet);
        }
        else
        {
            player.ReloadDefaultOutfit();
        }
    }

    private void Awake()
    {
        GameDataUser.Load();
        GameDataConstants.Load();

        btPlay.onClick.AddListener(ClickBtPlay);
        btSkin.onClick.AddListener(ClickBtSkin);
        btWeapon.onClick.AddListener(ClickBtWeapon);
    }

    private void Update()
    {
        txGold.text = GameDataUser.gold.ToString();
        if (Input.GetKeyUp(KeyCode.G))
        {
            Debug.Log("goldHave=" + GameDataUser.gold);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            int goldAdd = Random.Range(100, 1000);
            GameDataUser.AddGold(goldAdd);
            Debug.LogFormat("add={0}, goldHave={1}", goldAdd, GameDataUser.gold);
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            int goldConsume = Random.Range(100, 1000);
            GameDataUser.ConsumeGold(goldConsume);
            Debug.LogFormat("consume={0}, goldHave={1}", goldConsume, GameDataUser.gold);
        }
    }

    private void ClickBtPlay()
    {

    }

    private void ClickBtSkin()
    {
        popupSkin.gameObject.SetActive(true);
    }

    private void ClickBtWeapon()
    {
        popupWeapon.gameObject.SetActive(true);
    }
    public void ChangePlayer(SkinSetId id)
    {
        if (playerMap.ContainsKey(id) == false)
        {
            return;
        }
        for (int i = 0; i < playerMap.Count; i++)
        {
            playerMap[id].gameObject.SetActive(false);
        }

        if (player != null)
        {
            player.gameObject.SetActive(false);
        }
        player = playerMap[id];
        playerMap[id].gameObject.SetActive(true);
        playerMap[id].EquipHat((int)HatId.None);
    }

}

