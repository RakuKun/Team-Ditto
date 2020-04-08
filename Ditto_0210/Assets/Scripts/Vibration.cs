using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Vibration : MonoBehaviour
{
    float velocity;
     public Cell cell;
    string joystick;

    float vibrationRate;

    

     private MeshRenderer meshRender;
    // Start is called before the first frame update
    void Start()
    {
        velocity = cell.currentVel.magnitude;
        vibrationRate = 0f;
        joystick = cell.joystick;
        meshRender = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //velocity = cell.currentVel.magnitude;
       velocity = Mathf.Round(cell.currentVel.magnitude* 100f)/10f;
    
       
        if ((Input.GetKeyDown(joystick + " button 0")|| Input.GetKeyDown("space"))){      
           Debug.Log(velocity+" "+ vibrationRate);

           vibrationRate = 1f;
           
              
         }else{
             if (cell.isGrounded )
             { 
              if (Mathf.Approximately(velocity,0f)){ 
                  
                  if (vibrationRate >= 1f){
                    vibrationRate-=0.001f;
                    Debug.Log(vibrationRate);
                  }
          
               }else
               {
                //  if (vibrationRate <= 2f){
                //     vibrationRate+=0.004f;
                //   }
                vibrationRate = 2.5f;
               }
         }

         meshRender.sharedMaterial.SetFloat("_Vibration_Rate", vibrationRate);
         }
    }
}
