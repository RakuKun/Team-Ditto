using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildScript : MonoBehaviour
{
    void OnTriggerEnter(Collider other) 
    {
        Debug.Log("collide child");
        if (other.gameObject.tag == "Cell")
        {
        transform.parent.GetComponent<Cell_obi>().CollisionDetected(this, other.transform);
        
        }
       // IsGrounded();
        
    }
}
