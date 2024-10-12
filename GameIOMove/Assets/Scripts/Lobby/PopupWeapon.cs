using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PopupWeapon : MonoBehaviour
{


    public Image imgWeapon;
    public Text txWeaponRange;
    public Text txWeaponDamage;
    public Button btSellect;
    public Button btClose;
    public Text txSellect;
    public Text txNameWp;

    public Button leftButton;
    public Button rightButton;

    private int currentIndex = -1;
    private const string strEquipped = "Equipped";
    private const string strSellect = "Sellect";
    private void Awake()
    {
        currentIndex = 0;
        btClose.onClick.AddListener(ClickBtClose);

    }
    private void Start()
    {
        ShowWeaponDetails(currentIndex);
        leftButton.onClick.AddListener(ClickBtLeft);
        rightButton.onClick.AddListener(ClickBtRight);
        btSellect.onClick.AddListener(ClickBtSellect);
    }

    private void ClickBtClose()
    {
        gameObject.SetActive(false);
    }

    private WeaponData currentWeapon;

    private void ClickBtLeft()
    {
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = GameDataConstants.weapons.Count - 1;
        }
        ShowWeaponDetails(currentIndex);
    }
    private void ClickBtRight()
    {
        currentIndex++;
        if (currentIndex > GameDataConstants.weapons.Count - 1)
        {
            currentIndex = 0;
        }

        ShowWeaponDetails(currentIndex);
    }

    private void ClickBtSellect()
    {

        txSellect.text = strEquipped;
        btSellect.enabled = false;
        currentWeapon = GameDataConstants.weapons[currentIndex];
    }

    // This method will be called to show the weapon details on the popup
    private void ShowWeaponDetails(int index)
    {

        for (int i = 0; i < GameDataConstants.weapons.Count; i++)
        {
            WeaponData weapon = GameDataConstants.weapons[i];
            if (i == index)
            {
                currentIndex = i;
                imgWeapon.sprite = weapon.icon;
                txWeaponDamage.text = string.Format("Damage: {0}", weapon.damage.ToString());
                txWeaponRange.text = string.Format("Range:  {0}", weapon.range.ToString());
                txNameWp.text = weapon.weaponName;
                if (currentWeapon != weapon)
                {
                    txSellect.text = strSellect;
                }

            }
        }

    }
}

