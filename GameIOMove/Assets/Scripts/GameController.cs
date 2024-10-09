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

    public Player[] characterVariants; // Mảng chứa các prefab variant của nhân vật
    private Player currentCharacter;   // Nhân vật hiện tại

    // Start is called before the first frame update
    void Start()
    {
        GameDataConstants.Load();
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
        if (characterVariants.Length > 0)
        {
            currentCharacter = Instantiate(characterVariants[0], playerSpawn.position, Quaternion.identity); // Spawn nhân vật
            currentCharacter.tag = "TeamA";
            currentCharacter.SetJoystick(joystick); // Thiết lập joystick cho nhân vật
            // Thiết lập camera theo dõi nhân vật
            if (Camera.main != null && currentCharacter != null)
            {
                CameraController.Instance.SetTarget(currentCharacter.transform);
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
        if (index < characterVariants.Length)
        {

            // Lưu vị trí và hướng của nhân vật hiện tại
            Vector3 currentPosition = currentCharacter.transform.position;
            Quaternion currentRotation = currentCharacter.transform.rotation;

            // Xóa nhân vật hiện tại
            if (currentCharacter != null)
            {
                Destroy(currentCharacter.gameObject);
            }

            // Tạo nhân vật mới từ prefab variant
            currentCharacter = Instantiate(characterVariants[index], currentPosition, currentRotation);
            currentCharacter.tag = "TeamA";
            currentCharacter.SetJoystick(joystick); // Thiết lập joystick

            if (index != 0)
            {
                currentCharacter.headTransform = null;
            }

            // Cập nhật camera theo dõi nhân vật mới
            if (Camera.main != null)
            {
                CameraController.Instance.SetTarget(currentCharacter.transform);
            }
        }
    }

    private void CreateEnemy()
    {
        enemy = Instantiate(enemy);
        enemy.tag = "TeamB";
        enemy.transform.position = enemySpawn.position;
    }
}
