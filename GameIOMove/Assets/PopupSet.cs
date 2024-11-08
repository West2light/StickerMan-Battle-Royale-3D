using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupSet : MonoBehaviour
{
    public BoxSet prefabSet;
    public Transform content;

    private List<BoxSet> sets = new List<BoxSet>();

    public SkinSetId selectingSetID;

    private void Awake()
    {
        CreateSet();
    }

    private void OnEnable()
    {
        //LobbyManager.Instance.ChangePlayer(Ga);
    }

    private void CreateSet()
    {
        for (int i = 0; i < GameDataConstants.sets.Count; i++)
        {
            SetData data = GameDataConstants.sets[i];
            BoxSet instanceSet = Instantiate(prefabSet, content);
            instanceSet.SetInfo(this, data);
            sets.Add(instanceSet);
        }
        CheckHightLight();

    }
    private void CheckHightLight()
    {
        for (int i = 0; i < sets.Count; i++)
        {
            BoxSet set = sets[i];
            set.SetHightLight(selectingSetID == set.data.setId);
        }
    }
    public void OnSetSelected(SkinSetId setId)
    {
        selectingSetID = setId;
        CheckHightLight();
    }
}
