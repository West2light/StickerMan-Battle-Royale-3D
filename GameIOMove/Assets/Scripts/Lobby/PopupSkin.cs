using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkinTab
{

    Hat = 0,
    Pant = 1,
    Shield = 2,
    SkinSet = 3,
}

public class PopupSkin : MonoBehaviour
{
    public List<GameObject> popups;
    public SkinTab currentTab;

    private void Awake()
    {
        Show(SkinTab.Pant);
    }

    public void Show(SkinTab tab)
    {
        currentTab = tab;

        for (int i = 0; i < popups.Count; i++)
        {
            popups[i].SetActive(i == (int)currentTab);
        }
    }

}
