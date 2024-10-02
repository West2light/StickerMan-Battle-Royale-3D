using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    public Player player;
    public BaseWeapon wp;
    public Transform playerSpawn;
    public Joystick joystick;
    //public Transform handTransform;
    public Enemy enemy;
    public Transform enemySpawn;
    public LayerMask layerBody;

    public Transform maxX;
    public Transform maxZ;

    // Start is called before the first frame update
    void Start()
    {

        CreatePlayer();
        CreateEnemy();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CreatePlayer()
    {
        player = Instantiate(player);
        player.tag = "TeamA";
        player.transform.position = playerSpawn.position;
        player.SetJoystick(joystick);
        // Thiết lập camera theo dõi nhân vật
        if (Camera.main != null && player != null)
        {
            // Gọi phương thức theo dõi nhân vật
            CameraController.Instance.SetTarget(player.transform);
        }

    }
    private void CreateEnemy()
    {
        enemy = Instantiate(enemy);
        enemy.tag = "TeamB";
        enemy.transform.position = enemySpawn.position;
    }
}
