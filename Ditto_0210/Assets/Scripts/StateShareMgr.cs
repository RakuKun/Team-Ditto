using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RockVR.Video;

public class StateShareMgr : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject UI;
    public bool Share = false;
    public static StateShareMgr instance = null;
    void Awake() {
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }
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

    public void OpenUI()
    {
        UI.SetActive(true);
    }
    public void CloseUI()
    {
        UI.SetActive(false);
    }
}
