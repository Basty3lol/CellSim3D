using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cap_FPS_cam : MonoBehaviour
{
    // Start is called before the first frame update

    public int targetFrameRate = 30;
 
    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrameRate;
    }
}
