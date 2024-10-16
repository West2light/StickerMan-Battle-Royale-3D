using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PopupPant : MonoBehaviour
{
    public BoxPant prefabPant;

    public Transform content;

    public PantId pantIdSelecting;

    private List<BoxPant> pants = new List<BoxPant>();
    private void Start()
    {
        CreatePant();
    }

    private void CheckHightLight()
    {
        for (int i = 0; i < pants.Count; i++)
        {
            BoxPant pant = pants[i];
            pant.SetHightLight(pantIdSelecting == pant.data.id);
        }
    }
    private void CreatePant()
    {
        for (int i = 0; i < GameDataConstants.pants.Count; i++)
        {
            PantData pantData = GameDataConstants.pants[i];
            BoxPant instancePant = Instantiate(prefabPant, content);
            instancePant.SetInfo(this, pantData);
            pants.Add(instancePant);
        }
        CheckHightLight();
    }
    public void OnPantSelected(PantId pantId)
    {
        pantIdSelecting = pantId;
        CheckHightLight();
    }
}
