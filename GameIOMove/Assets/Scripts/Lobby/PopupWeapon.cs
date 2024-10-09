using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PopupWeapon : MonoBehaviour
{
    public GameObject weaponPopup;
    public void OnpenPopupWeapon()
    {
        weaponPopup.SetActive(true);
        // deactive button Skin, Play, Weapon
        LobbyManager.Instance.btPlay.gameObject.SetActive(false);
        LobbyManager.Instance.btSkin.gameObject.SetActive(false);
        LobbyManager.Instance.btWeapon.gameObject.SetActive(false);
    }

    public void ClosePopupWeapon()
    {
        weaponPopup.SetActive(false);
        // Active button Skin, Play, Weapon
        LobbyManager.Instance.btPlay.gameObject.SetActive(true);
        LobbyManager.Instance.btSkin.gameObject.SetActive(true);
        LobbyManager.Instance.btWeapon.gameObject.SetActive(true);
    }
}
