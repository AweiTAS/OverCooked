using CustomInput;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class CuttingCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler OnCut;
    public static event EventHandler OnAnyCut;

    [SerializeField] private CuttingRecipeSo[] recipes;
    [SerializeField] private int cuttingProgress;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject() && player.HasKitchenObject() && HasRecipe(player.GetKitchenObject().GetKitchenObjectSO()))
        {
            player.GetKitchenObject().SetKitchenObjectParent(this);
            cuttingProgress = 0;
            CuttingRecipeSo cuttingRecipeSo = GetCuttingRecipeSo(GetKitchenObject().GetKitchenObjectSO());
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressNormalized = cuttingProgress / (float)cuttingRecipeSo.cuttingProgressMax
            });
        }
        else if (HasKitchenObject() && !player.HasKitchenObject())
        {
            GetKitchenObject().SetKitchenObjectParent(player);
        }
        else if (HasKitchenObject() && player.HasKitchenObject())
        {
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                {
                    GetKitchenObject().DestorySelf();
                }
            }
        }
    }
    public override void InteractAlt(Player player)
    {
        if (HasKitchenObject() && HasRecipe(GetKitchenObject().GetKitchenObjectSO()))
        {
            cuttingProgress++;
            OnCut?.Invoke(this, EventArgs.Empty);
            OnAnyCut?.Invoke(this, EventArgs.Empty);
            CuttingRecipeSo cuttingRecipeSo = GetCuttingRecipeSo(GetKitchenObject().GetKitchenObjectSO());
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressNormalized = cuttingProgress / (float)cuttingRecipeSo.cuttingProgressMax
            });
            if (cuttingProgress == cuttingRecipeSo.cuttingProgressMax)
            {
                KitchenObjectSO kitchenObjectSO = GetKitchenObjectFromRecipe(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestorySelf();
                KitchenObject.SpawnKitchenObject(kitchenObjectSO, this);
            }
        }
    }
    public bool HasRecipe(KitchenObjectSO input)
    {
        CuttingRecipeSo recipe = GetCuttingRecipeSo(input);
        return recipe != null;
    }
    public KitchenObjectSO GetKitchenObjectFromRecipe(KitchenObjectSO input)
    {
        CuttingRecipeSo recipe = GetCuttingRecipeSo(input);
        return recipe != null ? recipe.output: null;
    }

    public CuttingRecipeSo GetCuttingRecipeSo(KitchenObjectSO input)
    {
        foreach (CuttingRecipeSo recipe in recipes)
        {
            if (recipe.input == input)
            {
                return recipe;
            }
        }
        return null;
    }
}
