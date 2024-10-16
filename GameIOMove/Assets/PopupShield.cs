using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupShield : MonoBehaviour
{
    public BoxShield prefabShield;
    public Transform content;

    private List<BoxShield> shields = new List<BoxShield>();

    public ShieldId shieldIdSelecting;
    private void Start()
    {
        CreateShield();
    }

    private void CreateShield()
    {
        for (int i = 0; i < GameDataConstants.shields.Count; i++)
        {
            ShieldData data = GameDataConstants.shields[i];
            BoxShield shield = Instantiate(prefabShield, content);
            shield.SetInfo(this, data);
            shields.Add(shield);
        }
        CheckHightLight();
    }

    private void CheckHightLight()
    {
        for (int i = 0; i < shields.Count; i++)
        {
            BoxShield shield = shields[i];
            shield.SetHightLight(shieldIdSelecting == shield.shieldData.shieldId);
        }
    }
    public void OnShieldSelectd(ShieldId shieldId)
    {
        shieldIdSelecting = shieldId;
        CheckHightLight();
    }
}
