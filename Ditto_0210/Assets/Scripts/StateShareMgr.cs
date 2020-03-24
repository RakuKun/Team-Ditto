using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RockVR.Video;

public class StateShareMgr : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("c"))
        {
            VideoCaptureCtrl.instance.StartCapture();
        }
        if (Input.GetKeyDown("x"))
        {
            VideoCaptureCtrl.instance.StopCapture();
        }
        if (Input.GetKeyDown("p"))
        {
            VideoPlayer.instance.SetRootFolder();
            VideoPlayer.instance.PlayVideo();
        }
    }
}
