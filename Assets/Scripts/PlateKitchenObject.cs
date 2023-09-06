using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    private List<KitchenObjectSO> listIngredients;
    [SerializeField] private List<KitchenObjectSO> listValidKitchenObjectSO;
    private void Awake()
    {
        this.listIngredients = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO ingredient)
    {
        if(listValidKitchenObjectSO!=null && listValidKitchenObjectSO.Contains(ingredient)) 
        {
            //ingredient is permitted to put on a plate
            if (!listIngredients.Contains(ingredient))
            {
                listIngredients.Add(ingredient);
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
}
