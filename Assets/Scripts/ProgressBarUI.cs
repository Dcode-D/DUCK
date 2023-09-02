using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject progressImplement;
    [SerializeField] private Image barImage;
    private IProgress hasProgress;

    private void Start()
    {
        hasProgress = progressImplement.GetComponent<IProgress>();
        if(hasProgress == null)
        {
            Debug.Log(progressImplement + "Does not have any Iprogress implmentation");
        }
        hasProgress.OnProgressChanged += CuttingCounter_OnProgressChanged;
        gameObject.SetActive(false);
    }

    private void CuttingCounter_OnProgressChanged(object sender, IProgress.ProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progress;
        gameObject.SetActive(true);
        if(e.progress == 0 ||e.progress==1)
        {
            gameObject.SetActive(false);
        }
    }
}
