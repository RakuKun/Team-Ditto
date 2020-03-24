using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Trigger : MonoBehaviour
{
    public delegate void TriggerHandler();
    public static event TriggerHandler HitTrigger;
    public GameObject Door;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
   private void OnTriggerEnter(Collider other) 
   {
        if (other.gameObject.tag == "Cell" || other.gameObject.tag == "MotherCell")
        {
            //HitTrigger();
            if (Door.gameObject.name == "Door1")
            Door.transform.DOLocalMoveX(-0.2f,0.4f);
            else if (Door.gameObject.name == "Door2")
            Door.transform.DOLocalMoveY(-0.2f,0.4f);
            
        }
   }
}
