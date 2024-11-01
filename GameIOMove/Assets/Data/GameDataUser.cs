using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class GameDataUser
{
    public static int gold;
    public static int equippedHat;
    public static int equippedWeapon;
    public static int equippedPant;
    public const string PREF_KEY_GOLD = "gold";

    public const string PREF_KEY_EQUIPPED_WEAPON = "equipped_weapon";
    public const string PREF_KEY_OWNED_WEAPON = "owned_weapon";

    public const string PREF_KEY_EQUIPPED_PANT = "equipped_pant";

    public const string PREF_KEY_EQUIPPED_HAT = "equipped_hat";
    public const string PREF_KEY_OWNED_HAT = "owned_hat";

    public static List<int> ownedHats = new List<int>();
    public static List<int> ownedWeapons = new List<int>();

    public static void Load()
    {
        gold = PlayerPrefs.GetInt(PREF_KEY_GOLD, 100);

        equippedWeapon = PlayerPrefs.GetInt(PREF_KEY_EQUIPPED_WEAPON);
        LoadOwnedWeapon();

        equippedHat = PlayerPrefs.GetInt(PREF_KEY_EQUIPPED_HAT);
        equippedPant = PlayerPrefs.GetInt(PREF_KEY_EQUIPPED_PANT);
    }

    public static void AddGold(int value)
    {
        gold += value;
        PlayerPrefs.SetInt(PREF_KEY_GOLD, gold);
        PlayerPrefs.Save();
    }

    public static void ConsumeGold(int value)
    {
        gold -= value;
        PlayerPrefs.SetInt(PREF_KEY_GOLD, gold);
        PlayerPrefs.Save();
    }

    // Hat
    public static bool IsOwnedHat(HatId id)
    {
        return ownedHats.Contains((int)id);
    }

    public static void BuyHat(HatId id)
    {
        if (ownedHats.Contains((int)id) == false)
        {
            ownedHats.Add((int)id);
            SaveOwendHat();
        }
    }

    public static void EquipHat(HatId id)
    {
        equippedHat = (int)id;
        PlayerPrefs.SetInt(PREF_KEY_EQUIPPED_HAT, equippedHat);
        PlayerPrefs.Save();
    }

    private static void SaveOwendHat()
    {
        string json = JsonConvert.SerializeObject(ownedHats);
        PlayerPrefs.SetString(PREF_KEY_OWNED_HAT, json);
        PlayerPrefs.Save();
    }

    private static void LoadOwnedHat()
    {
        //if (PlayerPrefs.HasKey(PREF_KEY_OWENED_HAT))
        //{
        //    string json = PlayerPrefs.GetString(PREF_KEY_OWENED_HAT);
        //    owenedWps = JsonConvert.DeserializeObject<List<int>>(json);
        //}
    }

    //Weapon
    public static void BuyWeapon(WeaponId id)
    {
        if (ownedWeapons.Contains((int)id) == false)
        {
            ownedWeapons.Add((int)id);
            string json = JsonConvert.SerializeObject(ownedWeapons);
            PlayerPrefs.SetString(PREF_KEY_OWNED_WEAPON, json);
            PlayerPrefs.Save();
        }
    }
    public static bool IsOwnedWeapon(WeaponId id)
    {
        return ownedWeapons.Contains((int)id);
    }

    private static void LoadOwnedWeapon()
    {
        string json = PlayerPrefs.GetString(PREF_KEY_OWNED_WEAPON);
        if (string.IsNullOrEmpty(json))
        {
            ownedWeapons = new List<int>();
            BuyWeapon(WeaponId.Hammer);
        }
        else
        {
            ownedWeapons = JsonConvert.DeserializeObject<List<int>>(json);
        }
    }

}
