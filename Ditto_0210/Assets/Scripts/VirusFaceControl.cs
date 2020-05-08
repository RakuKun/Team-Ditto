using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class VirusFaceControl : MonoBehaviour
{

    //public Cell cell;
   // string joystick;
    public Animator animator;

 

     
    // Start is called before the first frame update
    void Start()
    {

        // joystick = cell.joystick;

 
    }

    void Update()
    {
  
            textureThePlane();

    }

    public void Eaten()
    {
        animator.SetBool("IsEaten", true);
        animator.SetBool("IsEscaping", false);
        animator.SetBool("IsDizzy", false);
    }

    public void Escape()
    {
        animator.SetBool("IsEaten", false);
        animator.SetBool("IsEscaping", true);
        animator.SetBool("IsDizzy", false);
    }

    public void Dizzy()
    {
        animator.SetBool("IsEaten", false);
        animator.SetBool("IsEscaping", false);
        animator.SetBool("IsDizzy", true);
    }
    
    void textureThePlane(){
        //Virus is idle
         if (Input.GetKeyDown(KeyCode.V)){

            animator.SetBool("IsEaten", false);
            animator.SetBool("IsEscaping", false);
            animator.SetBool("IsDizzy", false);
     
         }        

  

        //Virus being eaten
         if (Input.GetKeyDown(KeyCode.Z)){

            animator.SetBool("IsEaten", true);
            animator.SetBool("IsEscaping", false);
            animator.SetBool("IsDizzy", false);
     
         }

        //Virus is escaping
         if (Input.GetKeyDown(KeyCode.X)){

            animator.SetBool("IsEaten", false);
            animator.SetBool("IsEscaping", true);
            animator.SetBool("IsDizzy", false);
     
         }

        //Virus is dizzy
         if (Input.GetKeyDown(KeyCode.C)){

            animator.SetBool("IsEaten", false);
            animator.SetBool("IsEscaping", false);
            animator.SetBool("IsDizzy", true);
     
         }
    }





}
