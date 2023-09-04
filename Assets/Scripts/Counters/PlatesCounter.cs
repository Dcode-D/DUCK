using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    [SerializeField] private float maxSpwanTime = 4f;
    [SerializeField] private KitchenObjectSO plateSO;
    [SerializeField] private int maxPlates = 4;

    private int plates = 0;
    private float spwanTime = 0;

    public event EventHandler<OnDishAmountChangedEventArgs> OnDishAmountChanged;
    public class OnDishAmountChangedEventArgs: EventArgs
    {
        public int amount;
    }

    public override void Interact(Player player)
    {
        if (plates > 0)
        {
            //if there are plates to take
            if (!player.HasKitchenObject())
            {
                //give player a plate
                KitchenObject.SpawnKitchenObject(plateSO.prefab, player);
                plates--;
                OnDishAmountChanged?.Invoke(this, new OnDishAmountChangedEventArgs { amount = plates });
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        
    }
    private void Update()
    {
        spwanTime += Time.deltaTime;
        if(spwanTime >= maxSpwanTime)
        {
            //clock hit time to spawn a dish
            //update UI to make it look like a dish is spwaned
            if(plates < maxPlates)
            {
                plates++;
                OnDishAmountChanged?.Invoke(this, new OnDishAmountChangedEventArgs { amount = plates });
            }
            //reset clock
            spwanTime = 0;
        }
    }
}
