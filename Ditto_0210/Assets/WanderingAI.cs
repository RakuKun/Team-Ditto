using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

 
public class WanderingAI : MonoBehaviour {
 
   public float wanderRadius;
   public float wanderTimer;
 
   private Transform target;
   private UnityEngine.AI.NavMeshAgent agent;
   private float timer;
   private Vector3 InitialPosition;
   public GameObject Cell;
   public Cell cell;
   public bool SeenCell = false;
   string joystick;
   public VirusFaceControl FaceControl;

 
   // Use this for initialization
   void OnEnable () {
    agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
    timer = wanderTimer;
    InitialPosition = transform.position;
    
   }
   void Start()
    {

         joystick = cell.joystick;

 
    }
 
   // Update is called once per frame
   void Update () {
    timer += Time.deltaTime;
 
    //  if (timer >= wanderTimer) {
    //   Vector3 newPos = RandomNavSphere(InitialPosition, wanderRadius, -1);
    //   agent.SetDestination(newPos);
    //   timer = 0;
    //  }

     if (!SeenCell && Vector3.Distance (this.transform.position, Cell.transform.position) < 25)
     {
         this.gameObject.GetComponent<Animator>().enabled = false;
         this.transform.DOMove(new Vector3(-1.8f,-104f,239.6f),2f,false);
         FaceControl.Escape(); 
         //agent.SetDestination(new Vector3(-0.4f,108.8f,239.6f));
         SeenCell = true;
     }

        if (SeenCell &&  Input.GetKey(joystick + " button 1"))
        {
            if (Vector3.Distance (this.transform.position, Cell.transform.position) > 9)
            {
                this.transform.DOMove(Cell.transform.position,1.5f,false);
            }
            FaceControl.Dizzy();
     
        }
   }
 
   public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) {
    Vector3 randDirection = Random.insideUnitSphere * dist;
 
    randDirection += origin;
 
    UnityEngine.AI.NavMeshHit navHit;
 
    UnityEngine.AI.NavMesh.SamplePosition (randDirection, out navHit, dist, layermask);
 
     return navHit.position;
   }
}






