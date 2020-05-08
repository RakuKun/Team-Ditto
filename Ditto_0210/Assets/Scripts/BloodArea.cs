using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodArea : MonoBehaviour
{
    public float strength;
    public Vector3 direction;
    private float counter = 0;
    public GameObject UpStream;
    public GameObject DownStream;
    private void OnTriggerStay(Collider other) 
    {
        if(other.tag == "MotherCell" || other.tag == "Cell")
        {
            //direction = this.transform.locaol
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

    void  Update() 
    {
        counter += Time.deltaTime;
        if (counter > 8)
        {
            counter = 0;
            StartCoroutine(ChangeDirection());
        }
    }

    IEnumerator ChangeDirection()
    {
        yield return new WaitForSeconds(0.1f);
        if (UpStream.active == true)
        {
            DownStream.SetActive(true);
            UpStream.SetActive(false);
        }
        else 
        {
            UpStream.SetActive(true);
            DownStream.SetActive(false);
        }
        this.direction.y = -this.direction.y;
    }
}

