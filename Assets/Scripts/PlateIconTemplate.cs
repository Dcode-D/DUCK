using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateIconTemplate : MonoBehaviour
{
    [SerializeField] private GameObject icon;
    public void SetKitchenObjectSO(KitchenObjectSO kitchenObjectSO)
    {
        var backgroundImage = icon.GetComponent<Image>();
        if (backgroundImage != null)
        {
            backgroundImage.sprite = kitchenObjectSO.sprite;
        }
        else
        {
            Debug.LogError("icon object does not contain any image");
        }
    }
}
