using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModeNormal : BaseMode
{
    public Timer timeCount;
    private void OnEnable()
    {
        GameDataConstants.Load();
        GameDataUser.Load();
        if (SceneManager.GetActiveScene().name == "GamePlay")
        {
            timeCount.ResetGameTime();
        }
    }
    private void Start()
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

            CreateEnemy();
        }
    }
    private void Update()
    {

        if (SceneManager.GetActiveScene().name == "GamePlay")
        {
            timeCount.TimeCountDown();
            if (timeCount.stopTimer)
            {
                for (int i = 0; i < GameController.Instance.enemies.Count; i++)
                {
                    GameController.Instance.enemies[i].ChangeState(BehaviourState.Win);
                }
                GameController.Instance.currentPlayer.ChangeState(BehaviourState.Dead);
            }

            if (GameController.Instance.enemies.Count == 0 || GameController.Instance.currentPlayer.state == BehaviourState.Dead)
            {
                timeCount.PauseTime();
            }

            if (GameController.Instance.currentPlayer.state == BehaviourState.Dead)
            {
                GameController.Instance.gameOver.gameObject.SetActive(true);
            }
        }

    }
    public override void BeginGame()
    {
        base.BeginGame();
    }
    public override void CreateEnemy()
    {
        base.CreateEnemy();
        for (int i = 0; i < 2; i++)
        {
            enemySpawn.position = new Vector3(
                Random.Range(GameController.Instance.currentPlayer.transform.position.x + Random.Range(5, 10), GameController.Instance.currentPlayer.transform.position.x + Random.Range(20, 40)),
                0f,
                Random.Range(GameController.Instance.currentPlayer.transform.position.z + Random.Range(5, 10), GameController.Instance.currentPlayer.transform.position.z + Random.Range(20, 40)));

            Enemy enemyInstance = Instantiate(GameController.Instance.enemy, enemySpawn.position, Quaternion.identity);
            enemyInstance.tag = "TeamB";
            GameController.Instance.enemies.Add(enemyInstance);
        }
    }
}