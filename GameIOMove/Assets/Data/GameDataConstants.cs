using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GameDataConstants
{
    public static List<WeaponData> weapons;
    public static List<HatData> hats;
    public static List<PantData> pants;
    public static List<ShieldData> shields;
    public static List<SetData> sets;
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
        if (pants == null)
        {
            pants = Resources.LoadAll<PantData>("GameData/Pants").ToList();
        }
        if (shields == null)
        {
            shields = Resources.LoadAll<ShieldData>("GameData/Shields").ToList();
        }
        if (sets == null)
        {
            sets = Resources.LoadAll<SetData>("GameData/Sets").ToList();
        }
    }

}

