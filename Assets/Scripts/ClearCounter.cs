using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public override void Interact(Player player)
    {
        if (kitchenObject==null)
        {
            Transform kitchenTransform = Instantiate(kitchenObjectSO.prefab,counterTop);
            kitchenObject = kitchenTransform.GetComponent<KitchenObject>();
            kitchenObject.SetParent(this);
        }
        else
        {
            if (!player.IsKitchenObject())
            {
                this.kitchenObject.SetParent(player);
                this.kitchenObject = null;
            }
            
        }
    }
}
