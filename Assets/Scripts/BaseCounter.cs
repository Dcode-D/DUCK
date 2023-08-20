using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] protected Transform counterTop;
    protected KitchenObject kitchenObject;
    public abstract void Interact(Player player);
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTop;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public bool IsKitchenObject()
    {
        return kitchenObject != null;
    }
}
