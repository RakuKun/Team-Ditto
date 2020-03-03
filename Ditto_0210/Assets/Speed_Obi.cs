using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

[RequireComponent(typeof(ObiActor))]
public class Speed_Obi : MonoBehaviour
{
    public float maxVel = 2;
    //ObiSolver solver;
    ObiActor actor;
    
    // Start is called before the first frame update
    void Awake()
    {
        actor = GetComponent<ObiActor>();
        //solver = this.GetComponent<Obi.ObiSolver>();
    }

    // Update is called once per frame
    void LateUpdate()
    {

            for (int i = 0; i < actor.solverIndices.Length; ++i){

				int k = actor.solverIndices[i];

				Vector3 vel = actor.solver.velocities[k];
                Debug.Log("actor "+k+" : "+vel);
                if (Input.GetKeyDown(KeyCode.V)){
                   
                   vel = Vector3.zero;
                         actor.solver.invMasses[k] = 0;
                       
                        Debug.Log("Ecceed! actor"+k+" vel.magnitude : "+vel.magnitude);
                }

			}

        // foreach ( ObiActor actor in solver.actors){
        //    int solverIndex = actor.solverIndices[0];
        //    Vector3 vel = solver.velocities[solverIndex];
        //    Debug.Log("actor "+solverIndex+" vel.magnitude : "+vel.magnitude);
        //    if (Input.GetKeyDown(KeyCode.Space)){
        //        solver.invMasses[solverIndex] = 0;
        //        solver.velocities[solverIndex] = Vector3.zero;
				
        //         solver.positions[solverIndex] = solver.transform.InverseTransformPoint(this.transform.position);
        //         Debug.Log("Ecceed! actor"+solverIndex+" vel.magnitude : "+vel.magnitude);
        //    }
        //    //Debug.Log("actor "+solverIndex+" : "+vel);
        // }
        
    }
}
