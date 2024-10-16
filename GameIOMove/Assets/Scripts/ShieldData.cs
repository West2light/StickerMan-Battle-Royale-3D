using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Shield-", menuName = "ScriptableObjects/ShieldData")]
public class ShieldData : ScriptableObject
{
    public ShieldId shieldId;
    public Sprite sprShield;
    public string name;

}
