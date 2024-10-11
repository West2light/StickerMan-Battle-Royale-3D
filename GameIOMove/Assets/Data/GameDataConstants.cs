using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GameDataConstants
{
    public static List<WeaponData> weapons;
    public static List<HatData> hats;

    public static void Load()
    {
        if (weapons == null)
        {
            weapons = Resources.LoadAll<WeaponData>("GameData/Weapons").ToList();
        }

        if (hats == null)
        {
            hats = Resources.LoadAll<HatData>("GameData/Hats").ToList();
        }
    }

}

