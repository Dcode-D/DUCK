using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plate;
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        plate.OnIngredientsAdded += Plate_OnIngredientsAdded;
    }

    private void Plate_OnIngredientsAdded(object sender, PlateKitchenObject.OnIngredientsAddedEventArgs e)
    {
        foreach(Transform icon in transform)
        {
            if(icon == iconTemplate)
            {
                //avoid destroying the initial icon
                continue;
            }
            //destroy all current displaying icon
            Destroy(icon.gameObject);
        }
        List<KitchenObjectSO> listKitchenObjectSO = plate.GetListIngredients();
        foreach(KitchenObjectSO kitchenObjectSO in listKitchenObjectSO)
        {
            Transform newicon = Instantiate(iconTemplate, transform);
            var newiconLogic = newicon.GetComponent<PlateIconTemplate>();
            newiconLogic.SetKitchenObjectSO(kitchenObjectSO);
            newicon.gameObject.SetActive(true);
        }
    }
}
