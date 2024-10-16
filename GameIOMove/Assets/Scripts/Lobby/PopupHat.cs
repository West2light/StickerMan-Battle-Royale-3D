using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupHat : MonoBehaviour
{
    public BoxHat prefabBoxhat;
    public Transform content;

    public HatId selectingHatId;

    private List<BoxHat> hats = new List<BoxHat>();

    private void Start()
    {
        CreateHats();
    }

    private void CheckHightLight()
    {
        for (int i = 0; i < hats.Count; i++)
        {
            BoxHat hat = hats[i];
            hat.SetHighlight(hat.data.hatId == selectingHatId);
        }

    }

    private void CreateHats()
    {
        for (int i = 0; i < GameDataConstants.hats.Count; i++)
        {
            HatData hatData = GameDataConstants.hats[i];

            BoxHat instanceBoxHat = Instantiate(prefabBoxhat, content);
            instanceBoxHat.SetInfo(this, hatData);

            hats.Add(instanceBoxHat);


        }

        CheckHightLight();
    }

    public void OnHatSelected(HatId hatId)
    {
        selectingHatId = hatId;
        CheckHightLight();
    }
    private void OnClick()
    {

    }
}
