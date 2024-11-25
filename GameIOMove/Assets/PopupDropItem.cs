using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PopupDropItem : MonoBehaviour
{
    public Image imgItem;
    public Button btExit;
    public Button btEquip;

    private void Awake()
    {
        btExit.onClick.AddListener(Deactive);
        CreateItem();
    }

    private void CreateItem()
    {
        WeaponData weaponData = new WeaponData();
        weaponData.id = (WeaponId)Random.Range(1, GameDataConstants.weapons.Count);
        if (GameDataUser.IsOwnedWeapon(weaponData.id))
        {
            return;
        }
        else
        {
            imgItem.sprite = weaponData.icon;
            btEquip.enabled = true;
        }

    }
    private void Deactive()
    {
        gameObject.SetActive(false);
    }


}
