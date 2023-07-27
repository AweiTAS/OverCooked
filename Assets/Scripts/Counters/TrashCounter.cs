using CustomInput;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnAnyTrashed;
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject()) {
            OnAnyTrashed?.Invoke(this, EventArgs.Empty);
            player.GetKitchenObject().DestorySelf(); 
        }
    }
}
