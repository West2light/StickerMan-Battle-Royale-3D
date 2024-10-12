using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupHat : MonoBehaviour
{
    public GameObject prefabBoxhat;
    public Transform content;
    private int currentIndex = -1;

    public HatId selectingHatId;

    private void Awake()
    {
        currentIndex = 0;
    }

    private void Start()
    {
        CreateHats();
        //ClickbtNext();
    }
    //private void ClickbtNext()
    //{
    //    currentIndex++;
    //    if (currentIndex > GameDataConstants.hats.Count)
    //    {
    //        currentIndex = 0;
    //    }
    //}
    private void Update()
    {

    }
    private void CreateHats()
    {
        for (int i = 0; i < GameDataConstants.hats.Count; i++)
        {
            HatData hat = GameDataConstants.hats[i];

            prefabBoxhat = Instantiate(prefabBoxhat);
            prefabBoxhat.transform.SetParent(content, false);
            BoxHatController controller = prefabBoxhat.GetComponent<BoxHatController>();
            controller.SetImages(hat.imageHat);
        }

        //currentIndex++;
        //if (currentIndex > GameDataConstants.hats.Count)
        //{
        //    currentIndex = 0;
        //}
    }
}
