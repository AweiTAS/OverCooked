using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plate;
    [SerializeField] private Transform iconTemplete;

    private void Awake()
    {
        iconTemplete.gameObject.SetActive(false);
    }
    private void Start()
    {
        plate.OnAddIngredent += Plate_OnAddIngredent;
    }

    private void Plate_OnAddIngredent(object sender, PlateKitchenObject.OnAddIngredentEventArgs e)
    {
        UpdateVisual();
    }
    private void UpdateVisual()
    {
        foreach(Transform child in transform)
        {
            if (child == iconTemplete) continue;
            Destroy(child.gameObject);
        }
        foreach(KitchenObjectSO kitchenObjectSO in plate.GetKitchenObjectSOList())
        {
            Transform icon = Instantiate(iconTemplete, transform);
            icon.gameObject.SetActive(true);
            icon.GetComponent<PlateIconSingleUI>().SetImage(kitchenObjectSO);
        }
    }
}
