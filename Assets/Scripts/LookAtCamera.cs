using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private enum Mode { 
        LookAt, 
        LookAtReverted,
        CameraForward,
        CamearaForwardReverted
    }

    [SerializeField]private Mode mode;
    private void LateUpdate()
    {
        switch (mode)
        {
            case Mode.LookAt:
                transform.LookAt(Camera.main.transform.position);
                break;
            case Mode.LookAtReverted:
                Vector3 camToPositionDir = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + camToPositionDir);
                break;

            case Mode.CameraForward:
                transform.forward = Camera.main.transform.forward;
                break;

            case Mode.CamearaForwardReverted:
                transform.forward = -Camera.main.transform.forward;
                break;

        }
        
    }
}
