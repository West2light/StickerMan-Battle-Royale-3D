using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxHatController : MonoBehaviour
{
    public Image imgHat;
    public Image imgLock;
    public Image imgHightLight;
    public void SetImages(Sprite newHatImage)
    {
        if (imgHat != null)
        {
            imgHat.sprite = newHatImage;
        }

        //if (imgLock != null)
        //{
        //    imgLock = newImgLock;
        //}
    }
}
