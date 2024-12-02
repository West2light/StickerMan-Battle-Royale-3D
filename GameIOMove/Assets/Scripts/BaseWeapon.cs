using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    public WeaponId id;
    public BaseBullet prefabBullet;

    public void CreateBullet(Character shooter)
    {
        BaseBullet bullet = BulletPool.Instance.GetBullet(prefabBullet.index);
        bullet.gameObject.SetActive(true);
        bullet.transform.position = shooter.throwPoint.position;
        bullet.transform.rotation = shooter.transform.rotation;
        bullet.distanceToDestroy = shooter.rangeAttack;
        bullet.shooter = shooter;
    }
}
