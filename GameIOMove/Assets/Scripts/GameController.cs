using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    public BaseWeapon wp;
    public Transform playerSpawn;
    public Joystick joystick;
    //public Transform handTransform;
    public Enemy enemy;
    public Transform enemySpawn;
    public LayerMask layerBody;

    public Transform maxX;
    public Transform maxZ;

    public Player[] playerVariants; // Mảng chứa các prefab variant của nhân vật
    public List<Enemy> enemies = new List<Enemy>();
    public Player currentPlayer;   // Nhân vật hiện tại
    public Enemy enemyInstance;

    private void OnEnable()
    {
        GameDataConstants.Load();
        GameDataUser.Load();
    }
    private void Start()
    {
        CreatePlayer();
        CreateEnemy();
    }


    //private void CreatePlayer()
    //{
    //    player = Instantiate(player);
    //    player.tag = "TeamA";
    //    player.transform.position = playerSpawn.position;
    //    player.SetJoystick(joystick);
    //    // Thiết lập camera theo dõi nhân vật
    //    if (Camera.main != null && player != null)
    //    {
    //        // Gọi phương thức theo dõi nhân vật
    //        CameraController.Instance.SetTarget(player.transform);
    //    }
    //    currentCharacter = player;
    //}

    //private void Update()
    //{
    //    // Ấn phím 1 để gọi ra set đồ đầu tiên (prefab variant)
    //    if (Input.GetKeyDown(KeyCode.Alpha1))
    //    {
    //        ChangeCharacter(0); // Set đồ đầu tiên
    //    }

    //    // Ấn phím 2 để gọi ra set đồ thứ hai
    //    if (Input.GetKeyDown(KeyCode.Alpha2))
    //    {
    //        ChangeCharacter(1); // Set đồ thứ hai
    //    }

    //    // Tương tự cho các set đồ khác
    //    if (Input.GetKeyDown(KeyCode.Alpha3))
    //    {
    //        ChangeCharacter(2); // Set đồ thứ ba
    //    }

    //    if (Input.GetKeyDown(KeyCode.Alpha4))
    //    {
    //        ChangeCharacter(3); // Set đồ thứ tư
    //    }

    //    if (Input.GetKeyDown(KeyCode.Alpha5))
    //    {
    //        ChangeCharacter(4); // Set đồ thứ năm
    //    }
    //}

    //private void ChangeCharacter(int index)
    //{
    //    // Xóa nhân vật hiện tại nếu có
    //    if (currentCharacter != null)
    //    {
    //        Destroy(currentCharacter);
    //    }

    //    // Instantiate nhân vật mới từ prefab variant
    //    if (index < characterVariants.Length)
    //    {
    //        currentCharacter = Instantiate(characterVariants[index]);

    //    }
    //}
    private void CreatePlayer()
    {
        // Tạo nhân vật đầu tiên từ characterVariants
        if (playerVariants.Length > 0)
        {
            currentPlayer = Instantiate(playerVariants[0], playerSpawn.position, Quaternion.identity); // Spawn nhân vật
            currentPlayer.tag = "TeamA";
            currentPlayer.SetJoystick(joystick); // Thiết lập joystick cho nhân vật
            // Thiết lập camera theo dõi nhân vật
            if (Camera.main != null && currentPlayer != null)
            {
                CameraController.Instance.SetTarget(currentPlayer.transform);
            }
        }

    }

    private void Update()
    {
        // Ấn phím để thay đổi set đồ (variant)
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeCharacter(0); // Set đồ đầu tiên
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeCharacter(1); // Set đồ thứ hai
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeCharacter(2); // Set đồ thứ ba
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeCharacter(3); // Set đồ thứ tư
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ChangeCharacter(4); // Set đồ thứ năm
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            ChangeCharacter(5); // Set đồ thứ năm
        }
    }

    private void ChangeCharacter(int index)
    {
        // Kiểm tra index có hợp lệ không
        if (index < playerVariants.Length)
        {

            // Lưu vị trí và hướng của nhân vật hiện tại
            Vector3 currentPosition = currentPlayer.transform.position;
            Quaternion currentRotation = currentPlayer.transform.rotation;

            // Xóa nhân vật hiện tại
            if (currentPlayer != null)
            {
                Destroy(currentPlayer.gameObject);
            }

            // Tạo nhân vật mới từ prefab variant
            currentPlayer = Instantiate(playerVariants[index], currentPosition, currentRotation);
            currentPlayer.tag = "TeamA";
            currentPlayer.SetJoystick(joystick); // Thiết lập joystick




            // Cập nhật camera theo dõi nhân vật mới
            if (Camera.main != null)
            {
                CameraController.Instance.SetTarget(currentPlayer.transform);
            }
        }
    }

    private void CreateEnemy()
    {

        for (int i = 0; i < 5; i++)
        {
            enemySpawn.position = new Vector3(
                Random.RandomRange(currentPlayer.transform.position.x + Random.Range(5, 10), currentPlayer.transform.position.x + Random.Range(20, 40)),
                0f,
                Random.RandomRange(currentPlayer.transform.position.z + Random.Range(5, 10), currentPlayer.transform.position.z + Random.Range(20, 40)));
            enemyInstance = Instantiate(enemy, enemySpawn.position, Quaternion.identity);
            enemyInstance.tag = "TeamB";
            enemies.Add(enemyInstance);
        }
        // enemyInstance.transform.position = enemySpawn.position;
    }
}
