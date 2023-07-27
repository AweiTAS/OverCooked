using CustomInput;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static CuttingCounter;

public class StoveCounter : BaseCounter, IHasProgress
{
    [SerializeField] private FryRecipeSO[] fryRecipes;

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public event EventHandler<OnStateChangeEventArgs> OnStateChangeEvent;
    public class OnStateChangeEventArgs : EventArgs{
        public bool state;
    }

    private FryRecipeSO fryRecipeSO;
    private float fryTimer;

    private void Update()
    {
        if (fryRecipeSO != null)
        {
            fryTimer += Time.deltaTime;
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressNormalized = fryTimer / fryRecipeSO.fryProgressMax
            });
            if (fryTimer >= fryRecipeSO.fryProgressMax)
            {
                fryTimer = 0f;
                GetKitchenObject().DestorySelf();
                KitchenObject.SpawnKitchenObject(fryRecipeSO.output, this);
                fryRecipeSO = GetFryingRecipeSo(GetKitchenObject().GetKitchenObjectSO());
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject() && player.HasKitchenObject() && HasRecipe(player.GetKitchenObject().GetKitchenObjectSO()))
        {
            player.GetKitchenObject().SetKitchenObjectParent(this);
            fryRecipeSO = GetFryingRecipeSo(GetKitchenObject().GetKitchenObjectSO());
            OnStateChangeEvent?.Invoke(this, new OnStateChangeEventArgs
            {
                state = true
            });
            fryTimer = 0f;
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressNormalized = fryTimer / fryRecipeSO.fryProgressMax
            });
        }
        else if (HasKitchenObject() && !player.HasKitchenObject())
        {
            GetKitchenObject().SetKitchenObjectParent(player);
            fryRecipeSO = null;
            OnStateChangeEvent?.Invoke(this, new OnStateChangeEventArgs
            {
                state = false
            });
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressNormalized = 0
            });
        }
        else if (HasKitchenObject() && player.HasKitchenObject())
        {
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                {
                    GetKitchenObject().DestorySelf();
                    fryRecipeSO = null;
                    OnStateChangeEvent?.Invoke(this, new OnStateChangeEventArgs
                    {
                        state = false
                    });
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = 0
                    });
                }
            }
            
        }
    }

    public bool HasRecipe(KitchenObjectSO input)
    {
        FryRecipeSO recipe = GetFryingRecipeSo(input);
        return recipe != null;
    }
    public KitchenObjectSO GetKitchenObjectFromRecipe(KitchenObjectSO input)
    {
        FryRecipeSO recipe = GetFryingRecipeSo(input);
        return recipe != null ? recipe.output : null;
    }
    public FryRecipeSO GetFryingRecipeSo(KitchenObjectSO input)
    {
        foreach (FryRecipeSO recipe in fryRecipes)
        {
            if (recipe.input == input)
            {
                return recipe;
            }
        }
        return null;
    }


}
