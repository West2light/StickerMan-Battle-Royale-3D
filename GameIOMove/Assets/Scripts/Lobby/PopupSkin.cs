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
    private void Start()
    {
        ShowPopup(currentTab);
    }

    public void ShowPopup(SkinTab newTab)
    {
        Debug.Log("ShowPopup called with tab: " + newTab);
        currentTab = newTab;
        UpdatePopups();
    }

    private void UpdatePopups()
    {
        Debug.Log("Updating popups... Current tab: " + currentTab);
        for (int i = 0; i < popups.Count; i++)
        {
            popups[i].gameObject.SetActive(false);
        }


        int tabIndex = (int)currentTab;
        if (tabIndex >= 0 && tabIndex < popups.Count)
        {
            popups[tabIndex].SetActive(true);
        }
    }
}
