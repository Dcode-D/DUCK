using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //doesn't have any kitchen object yet
            if (player.HasKitchenObject())
            {
                //player has kitchen object
                player.GetKitchenObject().SetParent(this);

            }
            else
            {
                //player has nothing 
            }
        }
        else
        {
            //already has a kitchen object
            if(!player.HasKitchenObject())
            {
                //player doesn't have any kitchen object
                this.kitchenObject.SetParent(player);
            }
            else
            {
                //player already had a kitchen object
                if(player.GetKitchenObject() is PlateKitchenObject)
                {
                    //player carrying a plate
                    var plateKitchenObject = player.GetKitchenObject() as PlateKitchenObject;
                    if (plateKitchenObject.TryAddIngredient(this.kitchenObject.GetKitchenObjectSO()))
                    {
                        this.kitchenObject.DestroySelf();
                    }
                }
                else
                {
                    //player not carrying a plate
                    if(this.kitchenObject is PlateKitchenObject)
                    {
                        //the kitchen object of this counter is a plate
                        var plateKitchenObject = this.kitchenObject as PlateKitchenObject;
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            
        }
    }

    public override void InteractAlternate(Player player)
    {
        
    }
}
