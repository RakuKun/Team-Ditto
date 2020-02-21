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
            other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.left*strength);
            Debug.Log("Enter wind zone");
        }
    }
}

