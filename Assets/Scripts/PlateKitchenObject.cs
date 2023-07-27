using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnAddIngredentEventArgs> OnAddIngredent;
    public class OnAddIngredentEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }

    [SerializeField] private List<KitchenObjectSO> vaildObject;

    private List<KitchenObjectSO> kitchenObjectSOList;

    private void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (!vaildObject.Contains(kitchenObjectSO)) return false;

        if (kitchenObjectSOList.Contains(kitchenObjectSO)) { 
            return false;
        }
        else
        {
            kitchenObjectSOList.Add(kitchenObjectSO);
            OnAddIngredent?.Invoke(this, new OnAddIngredentEventArgs
            {
                kitchenObjectSO = kitchenObjectSO
            });
            return true;
        }
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return kitchenObjectSOList;
    }
}
