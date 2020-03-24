using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharePoint : MonoBehaviour
{
    // Start is called before the first frame update
    public float DestinationY;
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other) 
   {
        if (other.gameObject.tag == "Cell" || other.gameObject.tag == "MotherCell")
        {
            //HitTrigger();
            other.gameObject.transform.position = new Vector3(other.gameObject.transform.position.x,other.gameObject.transform.position.y+DestinationY,other.gameObject.transform.position.z);
            
        }
   }
}
