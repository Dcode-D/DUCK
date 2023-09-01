using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    private IKitchenObjectParent kitchenObjectParent;
    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }
    public IKitchenObjectParent GetParent()
    {
        return kitchenObjectParent;
    }

    public void SetParent(IKitchenObjectParent parent)
    {
        if(this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.SetKitchenObject(null);
        }
        this.kitchenObjectParent = parent;
        this.kitchenObjectParent.SetKitchenObject(this);
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public void DestroySelf()
    {
        kitchenObjectParent.SetKitchenObject(null);
        Destroy(gameObject);
    }

    public static void SpawnKitchenObject(Transform prefab, IKitchenObjectParent parent)
    {
        Transform kitchenObjectTransform = Instantiate(prefab,parent.GetKitchenObjectFollowTransform());
        kitchenObjectTransform.GetComponent<KitchenObject>().SetParent(parent);
    }
}
