using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter,IProgress
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    private int cuttingProgress =0;

    public event EventHandler<IProgress.ProgressChangedEventArgs> OnProgressChanged;
    
    public event EventHandler OnCut;

    public override void Interact(Player player)
    {
        
        if (!HasKitchenObject())
        {
            //doesn't have any kitchen object yet
            if (player.HasKitchenObject())
            {
                //player has kitchen object
                //kitchen object can be cut
                OnProgressChanged?.Invoke(this, new IProgress.ProgressChangedEventArgs { progress = 0 });
                if (HasRecipeKitchenObject(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetParent(this);
                    cuttingProgress = 0;
                    
                }
                   
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
                OnProgressChanged.Invoke(this, new IProgress.ProgressChangedEventArgs { progress = 0 });
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
            KitchenObjectSO resultCuttingKitchenObject = GetResultKitchenObjectFromRecipe(kitchenObject.GetKitchenObjectSO());

            if(resultCuttingKitchenObject != null)
            {
            //object can be cut
            cuttingProgress++;
            var recipe = GetResultRecipe(kitchenObject.GetKitchenObjectSO());
                //notify progress changed 
            OnProgressChanged?.Invoke(this, new IProgress.ProgressChangedEventArgs
            {

                progress =(float) cuttingProgress/recipe.maxCut,
            });
            //invoke oncut when the object can be cut
            if(recipe != null&&recipe.maxCut>cuttingProgress)
                {
                    OnCut?.Invoke(this,EventArgs.Empty);
                }
            //if the maxcut for the recipe is reached, spawn new kitchen object
            if(cuttingProgress >= recipe.maxCut) 
                { 
                kitchenObject.DestroySelf();
                    KitchenObject.SpawnKitchenObject(recipe.output.prefab, this);
                }
            
            }
            
        }
    }

    private KitchenObjectSO GetResultKitchenObjectFromRecipe(KitchenObjectSO inputKitchenObjectSO)
    {
        var result = GetResultRecipe(inputKitchenObjectSO);
        return result ? result.output : null;
    }

    private bool HasRecipeKitchenObject(KitchenObjectSO inputKitchenObject)
    {
        var tmp = GetResultRecipe(inputKitchenObject);
        return tmp != null;
    }

    private CuttingRecipeSO GetResultRecipe(KitchenObjectSO input)
    {
        foreach (CuttingRecipeSO recipe in cuttingRecipeSOArray)
        {
            if (recipe.input == input)
            {
                return recipe;
            }
        }
        return null;
    }
}
