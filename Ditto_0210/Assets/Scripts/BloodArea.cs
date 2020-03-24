using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodArea : MonoBehaviour
{
    public float strength;
    public Vector3 direction;
    private void OnTriggerStay(Collider other) 
    {
        if(other.tag == "MotherCell" || other.tag == "Cell")
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(direction*strength);
            //other.gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.tag == "MotherCell" || other.tag == "Cell")
        {
            //other.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}

