using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    public WeaponId id;
    public BaseBullet prefabBullet;

    public void CreateBullet(Character shooter)
    {
        BaseBullet bullet = Instantiate(prefabBullet);
        bullet.transform.position = shooter.throwPoint.position;
        bullet.transform.rotation = shooter.transform.rotation;
        bullet.shooter = shooter;
    }
}
