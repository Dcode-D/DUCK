using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    private List<KitchenObjectSO> listIngredients;
    [SerializeField] private List<KitchenObjectSO> listValidKitchenObjectSO;
    private void Awake()
    {
        this.listIngredients = new List<KitchenObjectSO>();
    }

    public event EventHandler<OnIngredientsAddedEventArgs> OnIngredientsAdded;
    public class OnIngredientsAddedEventArgs: EventArgs
    {
        public KitchenObjectSO ingredient;
    }

    public bool TryAddIngredient(KitchenObjectSO ingredient)
    {
        if(listValidKitchenObjectSO!=null && listValidKitchenObjectSO.Contains(ingredient)) 
        {
            //ingredient is permitted to put on a plate
            if (!listIngredients.Contains(ingredient))
            {
                listIngredients.Add(ingredient);
                OnIngredientsAdded?.Invoke(this, new OnIngredientsAddedEventArgs { ingredient = ingredient });
                return true;
            }
            return false;
        }
        else
        {
            //ingredient is not permitted to put on a plate
            return false;
        }
    }
    public List<KitchenObjectSO> GetListIngredients()
    {
        return this.listIngredients;
    }
}
