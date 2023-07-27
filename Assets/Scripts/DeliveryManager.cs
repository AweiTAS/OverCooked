using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;

    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSo recipeListSo;
    [SerializeField] private float waitingTimerMax;
    [SerializeField] private int waitingRecipeMax;

    private List<RecipeSo> waitingRecipes;
    private float waitingTimer;
    private void Awake()
    {
        Instance = this;
        waitingRecipes = new List<RecipeSo>();
    }
    private void Update()
    {
        if(waitingRecipes.Count < waitingRecipeMax)
        {
            waitingTimer += Time.deltaTime;
            if(waitingTimer >= waitingTimerMax)
            {
                waitingTimer = 0;
                RecipeSo recipeSo = recipeListSo.recipeSOList[UnityEngine.Random.Range(0, recipeListSo.recipeSOList.Count)];
                waitingRecipes.Add(recipeSo);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        foreach(RecipeSo recipeSo in waitingRecipes)
        {
            if(recipeSo.kitchenObjectSOs.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                bool match = true;
                foreach(KitchenObjectSO ingredientInRecipe in recipeSo.kitchenObjectSOs)
                {
                    bool ingredientFound = false;
                    foreach(KitchenObjectSO ingredientInPlate in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        if(ingredientInPlate == ingredientInRecipe)
                        {
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound) match = false;
                }
                if (match)
                {
                    waitingRecipes.Remove(recipeSo);
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSo> GetWaitingRecipes()
    {
        return waitingRecipes;
    }
}
