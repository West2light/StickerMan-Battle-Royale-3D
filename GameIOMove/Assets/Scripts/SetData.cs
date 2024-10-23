using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Set-", menuName = "ScriptableObjects/SetData")]
public class SetData : ScriptableObject
{
    public SkinSetId setId;
    public Sprite sprSet;
    public string Name;
}
