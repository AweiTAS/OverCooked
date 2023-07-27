using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliverManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplete;

    private void Awake()
    {
        iconTemplete.gameObject.SetActive(false);
    }
    public void SetRecipe(RecipeSo recipeSo)
    {
        recipeNameText.text = recipeSo.name;
        foreach (Transform child in iconContainer)
        {
            if (child == iconTemplete) continue;
            Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO kitchenObjectSO in recipeSo.kitchenObjectSOs)
        {
            Transform iconTransform = Instantiate(iconTemplete, iconContainer);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
        }
    }

}
