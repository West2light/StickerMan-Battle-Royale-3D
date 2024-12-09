using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMode : MonoBehaviour
{

    // Nhân vật hiện tại

    public BaseWeapon wp;
    public Transform playerSpawn;


    public Transform enemySpawn;






    public static GameMode currentGameMode;

    private void OnEnable()
    {

    }
    public virtual void BeginGame()
    {

    }

    public virtual void EndGame()
    {

    }
    public virtual void CreateEnemy()
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
}

