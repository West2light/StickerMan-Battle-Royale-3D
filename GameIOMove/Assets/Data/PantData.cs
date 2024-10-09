using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Pant-", menuName = ("ScriptableObjects/PantData"))]
public class PantData : ScriptableObject
{
    public PantId id;
    public string name;
    public Sprite imagePant;
}
