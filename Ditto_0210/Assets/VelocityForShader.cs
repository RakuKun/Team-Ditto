using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class VelocityForShader : MonoBehaviour
{

    public Cell cell;
    string joystick;
    public Animator animator;

 

     
    // Start is called before the first frame update
    void Start()
    {

         joystick = cell.joystick;

 
    }

    void Update()
    {
  
            textureThePlane( joystick);

    }

    
    void textureThePlane(  string joystick){
  


         if (Input.GetAxisRaw(joystick) < 0){
   
         }

                  if (Input.GetAxisRaw(joystick) > 0){

         }


         if ((Input.GetKeyDown(joystick + " button 3")|| Input.GetKeyDown("space"))){

             animator.SetBool("eat", true);
     
         }
    }

    public void setEating(){
        bool check = animator.GetBool("eat");
        animator.SetBool("eat", false);
        if (check == true){
            animator.SetBool("eat", false);
        }
    }

    public void setSuprised(){
        bool check = animator.GetBool("suprised");
        animator.SetBool("suprised", false);
        if (check == true){
            animator.SetBool("suprised", false);
        }
    }


}
