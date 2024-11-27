using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PopupDropItem : MonoBehaviour
{
    public Image imgItem;
    public Button btExit;
    public Text txEquip;
    public Button btEquip;
    private WeaponData weaponData;
    private void Start()
    {
        btExit.onClick.AddListener(Deactive);
        btEquip.onClick.AddListener(OnClickBtEquip);
        CreateItem();
    }

    private void CreateItem()
    {
        List<WeaponData> unOwnWeapon = new List<WeaponData>();
        for (int i = 0; i < GameDataConstants.weapons.Count; i++)
        {
            WeaponData weaponData = GameDataConstants.weapons[i];
            if (GameDataUser.IsOwnedWeapon(weaponData.id) == false)
            {
                unOwnWeapon.Add(weaponData);
            }
        }
        int randomIndex = Random.Range(0, unOwnWeapon.Count);
        WeaponData selectedWeapon = unOwnWeapon[randomIndex];

        imgItem.sprite = selectedWeapon.icon;
        btEquip.enabled = true;

        weaponData = selectedWeapon;
    }
    private void Deactive()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene("GamePlay");
        //GameDataUser.Load();
        //GameDataConstants.Load();
    }
    private void OnClickBtEquip()
    {
        txEquip.text = "Equipped";
        btEquip.enabled = false;

        GameDataUser.equippedWeapon = (int)weaponData.id;
        PlayerPrefs.SetInt(GameDataUser.PREF_KEY_EQUIPPED_WEAPON, GameDataUser.equippedWeapon);
        PlayerPrefs.Save();

        GameDataUser.ownedWeapons.Add((int)weaponData.id);
        string json = JsonConvert.SerializeObject(GameDataUser.ownedWeapons);
        PlayerPrefs.SetString(GameDataUser.PREF_KEY_OWNED_WEAPON, json);
        PlayerPrefs.Save();
    }

}
