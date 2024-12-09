using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : Singleton<GameController>
{
    public Player currentPlayer;
    public BaseMode mode;
    public Dictionary<SkinSetId, Player> mapPlayer;
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
    }

    private void Start()
    {

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

    public virtual void UpdateScore()
    {
        currentPlayer.txPoint.text = point.ToString();
        currentPlayer.txAddPoint.gameObject.SetActive(false);
        currentPlayer.txAddPoint.gameObject.SetActive(true);
    }
    public virtual void ShowPopupDropItem()
    {
        dropItem.gameObject.SetActive(true);

    }
}