using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Hat-", menuName = "ScriptableObjects/HatData")]
public class HatData : ScriptableObject
{
    public HatId hatId;
    public string Name;
    public Sprite imageHat;
}
