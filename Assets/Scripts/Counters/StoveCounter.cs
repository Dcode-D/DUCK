using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter
{
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
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
            if (!player.HasKitchenObject())
            {
                //player doesn't have any kitchen object
                this.kitchenObject.SetParent(player);
            }
            else
            {
                //player already had a kitchen object
            }

        }
    }

    public override void InteractAlternate(Player player)
    {
        
    }
}
