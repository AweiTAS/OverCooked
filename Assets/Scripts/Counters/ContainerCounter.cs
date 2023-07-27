using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using CustomInput;

public class ContainerCounter: BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private SpriteRenderer kitchenObjectSpriteRenderer;
    private void Start()
    {
        kitchenObjectSpriteRenderer.sprite = kitchenObjectSO.sprite;
    }
    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
        }
    }
}
