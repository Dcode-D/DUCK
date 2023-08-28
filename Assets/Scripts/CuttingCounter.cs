using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
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
        if (HasKitchenObject())
        {
            //there is a kitchen object
            //Destroy current Kitchen object and give kitchen objects according to recipe
            KitchenObjectSO resultCuttingKitchenObject = GetResultRecipeSO(kitchenObject.GetKitchenObjectSO());

            if(resultCuttingKitchenObject != null)
            {
            kitchenObject.DestroySelf();
            Transform kitchenObjectTransform = Instantiate(resultCuttingKitchenObject?.prefab, GetKitchenObjectFollowTransform());
            kitchenObjectTransform.GetComponent<KitchenObject>().SetParent(this);
            Debug.Log(kitchenObjectTransform.localPosition);
            }
            
        }
    }

    private KitchenObjectSO GetResultRecipeSO(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach(CuttingRecipeSO recipe in cuttingRecipeSOArray)
        {
            if(recipe.input == inputKitchenObjectSO)
            {
                return recipe.output;
            }
        }
        return null;
    }
}
