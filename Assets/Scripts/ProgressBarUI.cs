using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image barImage;
    [SerializeField] private CuttingCounter cuttingCounter;

    private void Start()
    {
        cuttingCounter.OnProgressChanged += CuttingCounter_OnProgressChanged;
        gameObject.SetActive(false);
    }

    private void CuttingCounter_OnProgressChanged(object sender, CuttingCounter.ProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progress;
        gameObject.SetActive(true);
        if(e.progress == 0 ||e.progress==1)
        {
            gameObject.SetActive(false);
        }
    }
}
