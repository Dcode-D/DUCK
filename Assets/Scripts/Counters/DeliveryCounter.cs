using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    [SerializeField] private DeliveryManager _manager;
    private void Start()
    {
        _manager = DeliveryManager.instance;
    }
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            //player holding a kitchen object
            if(player.GetKitchenObject() is PlateKitchenObject)
            {
                // kitchen object is a plate
                _manager.Delivery(player.GetKitchenObject() as PlateKitchenObject);
                player.GetKitchenObject().DestroySelf();
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        
    }
}
