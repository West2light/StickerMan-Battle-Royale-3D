using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseMode : MonoBehaviour
{

    //public BaseWeapon wp;
    public Transform playerSpawn;
    public Transform enemySpawn;
    public static GameMode currentGameMode;


    protected GameController gameController;


    protected virtual void OnEnable()
    {
        GameDataConstants.Load();
        GameDataUser.Load();
    }
    public virtual void Init(GameController controller)
    {
        gameController = controller;
    }
    public virtual void BeginGame()
    {
        CreatePlayer();
        CreateEnemy();
    }

    public virtual void EndGame()
    {

    }

    protected virtual void CreatePlayer()
    {
        if (SceneManager.GetActiveScene().name == "GamePlay")
        {
            GameController.Instance.currentPlayer.txAddPoint.transform.parent.gameObject.SetActive(true);
            GameController.Instance.currentPlayer.rangeUI.transform.parent.gameObject.SetActive(true);
            GameController.Instance.point = 0;
            GameController.Instance.currentPlayer.txPoint.text = GameController.Instance.point.ToString();
            GameController.Instance.currentPlayer.gameObject.SetActive(true);
            if (GameDataUser.equippedSkinSet != (int)SkinSetId.None)
            {
                GameController.Instance.ChangePlayer((SkinSetId)GameDataUser.equippedSkinSet);
                SetUpPlayer();
            }
            else
            {
                GameController.Instance.currentPlayer.gameObject.SetActive(true);
                SetUpPlayer();
            }
        }
    }
    public virtual void CreateEnemy()
    {

    }
    public virtual void CreateEnemy(Transform spawn, string teamTag)
    {


    }
    protected virtual void SetUpPlayer()
    {
        GameController.Instance.currentPlayer.tag = "TeamA";
        GameController.Instance.currentPlayer.SetJoystick(GameController.Instance.joystick);
        if (Camera.main != null && GameController.Instance.currentPlayer != null)
        {
            CameraController.Instance.SetTarget(GameController.Instance.currentPlayer.transform);
        }
    }


    public virtual void OnDeadEnemy(Enemy enemy)
    {
        gameController.enemies.Remove(enemy);
        gameController.point += 1;
        gameController.UpdateScore();
        if (gameController.enemies.Count == 0)
        {
            gameController.currentPlayer.ChangeState(BehaviourState.Idle);
            gameController.ShowPopupDropItem();
        }

    }
    public virtual void OnDeadCurrentPlayer(Player player)
    {
        foreach (var enemy in GameController.Instance.enemies)
        {
            enemy.CheckTargetPoint(false);
            enemy.ChangeState(BehaviourState.Idle);
        }
    }
}

