using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObject_GameObject
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }

    [SerializeField] private List<KitchenObject_GameObject> pairList;
    [SerializeField] private PlateKitchenObject plate;
    private void Start()
    {
        plate.OnAddIngredent += Plate_OnAddIngredent;
        foreach (KitchenObject_GameObject KitObjGameObjPair in pairList)
        {
            KitObjGameObjPair.gameObject.SetActive(false);
        }
    }

    private void Plate_OnAddIngredent(object sender, PlateKitchenObject.OnAddIngredentEventArgs e)
    {
        foreach(KitchenObject_GameObject KitObjGameObjPair in pairList)
        {
            if(KitObjGameObjPair.kitchenObjectSO == e.kitchenObjectSO)
            {
                KitObjGameObjPair.gameObject.SetActive(true);
            }
        }
    }
}
