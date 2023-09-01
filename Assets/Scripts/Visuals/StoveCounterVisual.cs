using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject heatingGameObject;
    [SerializeField] private GameObject particlesGameObject;
    [SerializeField] private StoveCounter stoveCounter;
    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        if(e.state == StoveCounter.State.Burned||e.state == StoveCounter.State.Idle)
        {
            heatingGameObject.SetActive(false);
            particlesGameObject.SetActive(false);
        }
        if(e.state == StoveCounter.State.Frying|| e.state == StoveCounter.State.Fried)
        {
            heatingGameObject.SetActive(true);
            particlesGameObject.SetActive(true);
        }
    }
}
