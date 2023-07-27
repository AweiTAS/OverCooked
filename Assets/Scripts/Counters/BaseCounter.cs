using CustomInput;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    public static event EventHandler OnDrop;
    [SerializeField] protected Transform topPoint;

    private KitchenObject kitchenObject;
    public virtual void Interact(Player player)
    {
        Debug.Log("BaseCounter::Interact() this should not happen");
    }
    public virtual void InteractAlt(Player player)
    {
        Debug.Log("BaseCounter::InteractAlt() this should not happen");
    }

    public virtual Transform GetKitchenObjectFollowTransform()
    {
        return topPoint;
    }
    public virtual KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public virtual void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if (kitchenObject != null)
        {
            OnDrop?.Invoke(this, EventArgs.Empty);
        }
    }
    public virtual void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    public virtual bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
