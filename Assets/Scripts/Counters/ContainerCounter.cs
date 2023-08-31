using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public event EventHandler OnGetKitchenObject;
    
    
    public override void Interact(Player player)
    {
        if (kitchenObject == null)
        {
            if (!player.HasKitchenObject()) {
            Transform kitchenTransform = Instantiate(kitchenObjectSO.prefab);
            kitchenObject = kitchenTransform.GetComponent<KitchenObject>();
            kitchenObject.SetParent(player);
            SetKitchenObject(null);
            OnGetKitchenObject?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        
    }

    public Sprite GetKitchenObjectSOSprite()
    {
        return kitchenObjectSO.sprite;
    }
}
