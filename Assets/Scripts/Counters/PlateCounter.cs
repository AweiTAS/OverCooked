using CustomInput;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter
{
    public event EventHandler OnPlateGenerateEvent;
    public event EventHandler OnPlateRemoveEvent;
    [SerializeField] private KitchenObjectSO plateSO;

    private float spawnTimer;
    [SerializeField] private float spawnTimerMax;
    private int plateCount;
    [SerializeField] private int plateCountMax;

    private void Update()
    {
        if(plateCount < plateCountMax)
        {
            spawnTimer += Time.deltaTime;
            if(spawnTimer > spawnTimerMax)
            {
                spawnTimer = 0;
                plateCount++;
                OnPlateGenerateEvent?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject() && plateCount>0)
        {
            plateCount--;
            KitchenObject.SpawnKitchenObject(plateSO, player);
            OnPlateRemoveEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
