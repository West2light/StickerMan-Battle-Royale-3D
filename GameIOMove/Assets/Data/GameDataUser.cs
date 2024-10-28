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
    public const string PREF_KEY_EQUIPPED_PANT = "equipped_pant";
    public const string PREF_KEY_EQUIPPED_HAT = "equipped_hat";
    public const string PREF_KEY_OWENED_HAT = "owened_hat";

    public static List<int> owernedHats = new List<int>();

    public static void Load()
    {
        gold = PlayerPrefs.GetInt(PREF_KEY_GOLD, 1000);
        equippedHat = PlayerPrefs.GetInt(PREF_KEY_EQUIPPED_HAT, (int)HatId.Cowboy);
        equippedWeapon = PlayerPrefs.GetInt(PREF_KEY_EQUIPPED_WEAPON, (int)WeaponId.Hammer);
        equippedPant = PlayerPrefs.GetInt(PREF_KEY_EQUIPPED_PANT, (int)PantId.Batman);

        // Load hat đang equpiped + owerned hat
        LoadOwernedHat();
        for (int i = 0; i < owernedHats.Count; i++)
        {
            Debug.Log("List of owernedHat: " + owernedHats[i]);
        }
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
        return owernedHats.Contains((int)id);
    }

    public static void BuyHat(HatId id)
    {
        if (owernedHats.Contains((int)id) == false)
        {
            owernedHats.Add((int)id);
            SaveOwernedHat();
        }
    }

    public static void EquipHat(HatId id)
    {
        equippedHat = (int)id;
        PlayerPrefs.SetInt(PREF_KEY_EQUIPPED_HAT, equippedHat);
        PlayerPrefs.Save();
    }
    public static void SaveOwernedHat()
    {
        string json = JsonConvert.SerializeObject(owernedHats);
        PlayerPrefs.SetString(PREF_KEY_OWENED_HAT, json);
        PlayerPrefs.Save();
    }
    public static void LoadOwernedHat()
    {
        if (PlayerPrefs.HasKey(PREF_KEY_OWENED_HAT))
        {
            string json = PlayerPrefs.GetString(PREF_KEY_OWENED_HAT);
            owernedHats = JsonConvert.DeserializeObject<List<int>>(json);
        }

    }

    //public static void BuyHat()
    //{
    //    for (int i = 0; i < GameDataConstants.hats.Count; i++)
    //    {
    //        HatData hat = GameDataConstants.hats[i];
    //        if (IsOwnedHat(hat.hatId) == false)
    //        {
    //            if (gold >= hat.price)
    //            {
    //                gold -= hat.price;
    //                PlayerPrefs.SetInt(PREF_KEY_GOLD, gold);
    //                PlayerPrefs.Save();

    //                owernedHats.Add(hat.hatId);
    //                PlayerPrefs.SetInt(PREF_KEY_OWENED_HAT, (int)HatId.Cowboy);
    //                PlayerPrefs.Save();
    //                Debug.Log("Bought!!!");
    //            }
    //            else
    //            {
    //                Debug.Log("Not enough money!!!");
    //            }
    //        }
    //    }

    //}
}
