using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectGround : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isGrounded = true;
    //public GameObject AnotherFoot;
    void Start()
    {
        
    }
    private void Update() 
    {
        if (Physics.Raycast(transform.position, -Vector3.up, this.transform.parent.localScale.y/2 + 0.05f))
        {
            isGrounded = true;
            //this.transform.parent.GetComponent<Cell>().isGrounded = true;
            //this.transform.parent.GetComponent<Cell>().isFirstJump = false;
        }
        else isGrounded = false;
        
    }

    // Update is called once per frame
    
}
