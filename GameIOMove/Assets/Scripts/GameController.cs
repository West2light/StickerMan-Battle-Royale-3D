using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : Singleton<GameController>
{
    public Player currentPlayer;
    public BaseMode mode;
    public BaseMode[] prefabMode;
    public Dictionary<SkinSetId, Player> mapPlayer;
    public Dictionary<GameMode, BaseMode> mapGameMode;
    public Player[] playerVariants;

    public LayerMask layerBody;
    public Enemy enemy;
    public Transform maxX;
    public Transform maxZ;
    public List<Enemy> enemies = new List<Enemy>();
    public Joystick joystick;
    public int point;
    public PopupDropItem dropItem;
    public GameOver gameOver;


    private void Awake()
    {
        mapPlayer = new Dictionary<SkinSetId, Player>
        {
            {SkinSetId.DeadPool, playerVariants[0]},
            {SkinSetId.Angel, playerVariants[1]},
            {SkinSetId.Devil, playerVariants[2]},
            {SkinSetId.Thor, playerVariants[3]},
            {SkinSetId.Witch, playerVariants[4]}
        };
        mapGameMode = new Dictionary<GameMode, BaseMode>
        {
            {GameMode.Normal, prefabMode[0]},
            {GameMode.Team, prefabMode[1]}

        };
    }

    private void Start()
    {
        SetMode<GameModeNormal>();
    }

    private void Update()
    {

    }
    public virtual void ChangePlayer(SkinSetId id)
    {

        if (currentPlayer != null)
        {
            currentPlayer.gameObject.SetActive(false);
        }
        currentPlayer = mapPlayer[id];
        mapPlayer[id].gameObject.SetActive(true);
        mapPlayer[id].EquipHat((int)HatId.None);

    }

    public void UpdateScore()
    {
        currentPlayer.txPoint.text = point.ToString();
        currentPlayer.txAddPoint.gameObject.SetActive(false);
        currentPlayer.txAddPoint.gameObject.SetActive(true);
    }
    public void ShowPopupDropItem()
    {
        dropItem.gameObject.SetActive(true);
    }
    public void SetMode<T>() where T : BaseMode
    {
        if (mode != null)
        {
            mode.EndGame();
            mode.gameObject.SetActive(false);
        }

        // Tạo chế độ mới
        if (typeof(T) == typeof(GameModeNormal))
        {
            mode.gameObject.SetActive(true);
            mapGameMode[GameMode.Normal].gameObject.SetActive(true);
            mode = prefabMode[(int)GameMode.Normal];
        }
        else if (typeof(T) == typeof(GameModeTeam))
        {
            mode.gameObject.SetActive(true);
            mapGameMode[GameMode.Team].gameObject.SetActive(true);
            mode = prefabMode[(int)GameMode.Team];
        }

        // Khởi tạo chế độ mới
        mode.Init(this);
        mode.BeginGame();
    }
}