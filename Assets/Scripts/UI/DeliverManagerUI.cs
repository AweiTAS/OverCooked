using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform templete;

    private void Awake()
    {
        templete.gameObject.SetActive(false);
    }
    private void Start()
    {
        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeSpawned(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeCompleted(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach(Transform child in container)
        {
            if (child == templete) continue;
            Destroy(child.gameObject);
        }

        foreach(RecipeSo recipeSo in DeliveryManager.Instance.GetWaitingRecipes())
        {
            Transform recipeTransform = Instantiate(templete, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliverManagerSingleUI>().SetRecipe(recipeSo);
        }
    }
}
