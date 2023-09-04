using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter plateCounter;
    [SerializeField] private Transform counterTop;
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    Stack<Transform> stackPlates;
    private void Start()
    {
        stackPlates = new Stack<Transform>();
        plateCounter.OnDishAmountChanged += PlateCounter_OnDishAmountChanged;
    }

    private void PlateCounter_OnDishAmountChanged(object sender, PlatesCounter.OnDishAmountChangedEventArgs e)
    {
        while(stackPlates.Count>0)
        {
            Transform t = stackPlates.Pop();
            Destroy(t.GetChild(0).gameObject);
        }
        for(int i = 0;i< e.amount; i++)
        {
            Transform t = Instantiate(plateKitchenObjectSO.prefab, counterTop);
            float plateOffSetY = 0.1f;
            t.localPosition = new Vector3(0,plateOffSetY*i,0);
            stackPlates.Push(t);
        }
    }
}
