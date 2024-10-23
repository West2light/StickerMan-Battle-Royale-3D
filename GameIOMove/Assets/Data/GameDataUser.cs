using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class GameDataUser
{
    public static int gold;

    public const string PREF_KEY_GOLD = "gold";

    public const string PREF_KEY_EQUIPPED_WEAPON = "equipped_weapon";
    public const string PREF_KEY_EQUIPPED_PANT = "equipped_pant";
    public const string PREF_KEY_EQUIPPED_HAT = "equipped_hat";

    public static void Load()
    {
        gold = PlayerPrefs.GetInt(PREF_KEY_GOLD, 1000);
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
        return false;
    }

    public static void BuyHat(HatId id)
    {
        for (int i = 0; i < GameDataConstants.hats.Count; i++)
        {
            HatData hat = GameDataConstants.hats[i];
            if (IsOwnedHat(hat.hatId) == false)

            {
                if (gold >= hat.price)
                {
                    gold -= hat.price;
                    PlayerPrefs.SetInt(PREF_KEY_GOLD, gold);
                    PlayerPrefs.Save();
                }
            }
        }
    }
}
