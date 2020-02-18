using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform cell;
    void Start()
    {
        if (!cell)
        {
            Debug.LogWarning("Cell not assigned! Trying to find one.");
            try
            {
                cell = GameObject.Find("Cell").transform;
            }
            catch(Exception e) {
                Debug.LogWarning("error! "+e);
                enabled = false;
            }
        }
    }
    void LateUpdate()
    {
        transform.position = new Vector3(cell.position.x, cell.position.y, transform.position.z);
    }
}
