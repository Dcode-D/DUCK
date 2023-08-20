using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter counter;
    [SerializeField] private GameObject[] counterSelectedArray;
    // Start is called before the first frame update
    void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if(e.selectedCounter == counter)
        {
            foreach(GameObject selectedVisual in counterSelectedArray)
                selectedVisual.SetActive(true);
        }
        else
        {
            foreach (GameObject selectedVisual in counterSelectedArray)
                selectedVisual.SetActive(false);
        }
    }
}
