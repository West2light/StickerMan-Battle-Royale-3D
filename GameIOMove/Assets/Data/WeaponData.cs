using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon-", menuName = "ScriptableObjects/WeaponData")]
public class WeaponData : ScriptableObject
{
    public WeaponId id;
    public string weaponName;
    public Sprite icon;
    public float damage;
    public float range;
}
