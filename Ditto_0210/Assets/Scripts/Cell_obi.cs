using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Obi;

[RequireComponent(typeof(ObiActor))]

public class Cell_obi : MonoBehaviour
{
    public GameObject CellObject;
    public Vector3 OriginalScale;
    Vector3 previous;
    public Rigidbody rb;
    float JoyStickA;
    public string joystick;
  

    public bool isGrounded = true;
    public bool FinishJump = true;
    protected int NumOfPlayer = 1;
    public int JumpPower = 3500;
    public float JumpHeight = 1.5f;
    public float JumpDistance = 1;
    public float MoveSpeed = 5;
    public int CellLevel = 1;



    public int basePhase = 2;
    public float intensity = 5;
    private int phase = 0;
    ObiSolver solver;
    Obi.ObiSolver.ObiCollisionEventArgs collisionEvent;
    
    
    // Start is called before the first frame update
    void Awake() 
     {

       // solver = transform.parent.GetComponent<Obi.ObiSolver>();
        //OriginalScale = this.transform.localScale;
        //rb = this.GetComponent<Rigidbody>();
        
        
    }

    // void OnEnable () {
	// 	solver.OnCollision += Solver_OnCollision;
	// }

	// void OnDisable(){
	// 	solver.OnCollision -= Solver_OnCollision;
	// }


    // Update is called once per frame
    void Update()
    {
        JoyStickA = Input.GetAxisRaw(joystick);
        Debug.Log(Input.GetJoystickNames()[0] +" 2:"+Input.GetJoystickNames()[1]);
        // if (Input.GetAxisRaw(joystick)!= 0 && isGrounded)
        // {
        //     LazyFollow(this.transform.position + Vector3.right * Time.deltaTime*JoyStickA*40);
        // }

        //Key input for testing
        if (Input.GetAxisRaw(joystick)!= 0 && isGrounded )
        {

            //GetComponent<ObiActor>().AddForce( new Vector3(-2, 0.5f, 0)*intensity,ForceMode.VelocityChange);
            GetComponent<ObiActor>().AddForce( JoyStickA*Vector3.right*0.5f,ForceMode.VelocityChange);
            //transform.Translate(Vector3.left*Time.deltaTime*MoveSpeed,Space.World);
        }
            if (Input.GetKeyDown(KeyCode.W) )
        {

            GetComponent<ObiActor>().AddForce( new Vector3(0, 0.5f, 2f)*intensity,ForceMode.VelocityChange);
            //transform.Translate(Vector3.left*Time.deltaTime*MoveSpeed,Space.World);
        }
              if (Input.GetKeyDown(KeyCode.S) )
        {

            GetComponent<ObiActor>().AddForce( new Vector3(0, 0.5f, -2f)*intensity,ForceMode.VelocityChange);
            //transform.Translate(Vector3.left*Time.deltaTime*MoveSpeed,Space.World);
        }
        if (Input.GetKeyDown(KeyCode.D) )
        {
            GetComponent<ObiActor>().AddForce(new Vector3(2, 0.5f, 0)*intensity,ForceMode.VelocityChange);
            //transform.Translate(Vector3.right*Time.deltaTime*MoveSpeed,Space.World);
        }
       
        
        //split
        if (Input.GetKeyDown(joystick + " button 2"))
        {
            Split();
           
        }
  
        //change scale
       // Stretching();
        
    
        if (Input.GetKeyDown(joystick + " button 0")  || Input.GetKeyDown("space"))
        {
            IsGrounded();
            if (isGrounded)
            {
                isGrounded = false;
                FinishJump = false;
                //rb.useGravity = false;
            
                StartCoroutine(Jump(Vector2.one));
            }
            
        }
        
        
    }
    void IsGrounded()
    {
        if (Physics.Raycast(transform.position, -Vector3.up, this.transform.localScale.y/2 + 0.05f))
        {
            isGrounded = true;
            Debug.Log("Ground");
        }
    
    }

    public void Stretching()
    {
        var velocity = (this.transform.position - previous) / Time.deltaTime;
        if (velocity.x>10) velocity.x = 10;
        else if (velocity.x<-10) velocity.x = -10;
        if (velocity.y>10) velocity.y = 10;
        else if (velocity.y<-10) velocity.y = -10;
        if (velocity.z>10) velocity.z = 10;
        else if (velocity.z<-10) velocity.z = -10;
        this.transform.localScale = new Vector3 (Mathf.Abs(velocity.x/30),
                                                 Mathf.Abs(velocity.y/30),
                                                 Mathf.Abs(velocity.z/30)
                                                ) + OriginalScale;
        previous = this.transform.position;
    }
    

    public void LazyFollow(Vector3 Destination)
    {
        //this.transform.DOMoveX(Destination.x,1f);
        // Vector3 Direction = (Destination - this.transform.position).normalized;
        // Direction.z =0;
        // Quaternion XLookRotation = Quaternion.LookRotation(Destination, transform.up) * Quaternion.Euler(new Vector3(0, 90, 0));
        //rb.AddForce(Vector3.right *JoyStickA*150);
        transform.Translate(Vector3.right*Time.deltaTime*JoyStickA*MoveSpeed,Space.World);
            
    }

    public IEnumerator Jump(Vector3 direction)
    {
        
        if (Input.GetAxisRaw(joystick) > 0)
            direction = new Vector3(JumpDistance,JumpHeight,0);
        else if (Input.GetAxisRaw(joystick) < 0)
            direction = new Vector3(-JumpDistance,JumpHeight,0);
        else 
            direction = new Vector3(0,JumpHeight,0);
        Debug.Log("Jump");
        GetComponent<ObiActor>().AddForce(direction*JumpPower , ForceMode.VelocityChange);
        yield return new WaitForSeconds(0.1f);
        //FinishJump = true;
    }

    protected void Split()
    {
        Vector3 offset = new Vector3( 1,0,0);
        if (this.transform.localScale.x>0.4f && this.transform.localScale.y>0.4f &&this.transform.localScale.z>0.4f)
        {

            CellManager.instance.PlayerNumber ++;
            GameObject newCell = Instantiate(CellObject, this.transform.position+offset,this.transform.rotation);
            newCell.transform.parent = this.transform.parent;
            newCell.GetComponent<ObiActor>().SetPhase(basePhase + phase);
            newCell.GetComponent<Cell_obi>().joystick = "joystick " + CellManager.instance.PlayerNumber.ToString();
            // this.transform.localScale = OriginalScale*0.8f;
            // OriginalScale = this.transform.localScale;
            // newCell.transform.localScale = OriginalScale;
            // newCell.GetComponent<Cell_obi>().OriginalScale = this.transform.localScale;
            // //newCell.GetComponent<ObiActor>().useGravity = true;
            // //this.GetComponent<Rigidbody>().mass = OriginalScale.z * CellManager.instance.DefaultMass;
            // //newCell.GetComponent<Rigidbody>().mass = newCell.transform.localScale.z * CellManager.instance.DefaultMass;
            // MoveSpeed = CellManager.instance.DefaultSpeed/(OriginalScale.z+CellManager.instance.SpeedDamp);
            // newCell.GetComponent<Cell_obi>().MoveSpeed = CellManager.instance.DefaultSpeed/(newCell.transform.localScale.z+CellManager.instance.SpeedDamp);
            phase++;
        }
    }
    // void Solver_OnCollision (object sender, Obi.ObiSolver.ObiCollisionEventArgs e)
	// {
	// 	foreach (Oni.Contact contact in e.contacts)
	// 	{
	// 		// this one is an actual collision:
	// 		if (contact.distance < 0.01)
	// 		{
	// 			Component collider;
    //            if (ObiCollider.idToCollider.TryGetValue(contact.other, out collider))
    //            {
    //                Collider ContactCollider = ObiCollider.idToCollider[contact.other] as Collider;
    //                if (ContactCollider.transform.gameObject.CompareTag ("Cell")){
    //                      if (Input.GetKey(joystick + " button 3"))
    //                      {
    // //             Merge(other.gameObject);
    //                      }
    //                    //Debug.Log(ContactCollider.transform.gameObject.name);
    //                    Merge(ContactCollider.transform.gameObject);

    //                }

                   
    //            }
	// 		}
	// 	}
	// }

    public void CollisionDetected(ChildScript childScript, Transform other )
     {
         Debug.Log("child collided");
         if (Input.GetKey(joystick + " button 3"))
            {
                Debug.Log("merge "+other);
                Destroy(other.parent.gameObject);
            }
     } 

    

    protected void Merge(GameObject EatenCell)
    {

        Debug.Log("merge");
        Destroy(EatenCell);
        // if (this.transform.localScale.x > EatenCell.transform.localScale.x)   
        // { 
        //     CellManager.instance.PlayerNumber --;
        //     float AddedSize = OriginalScale.z/1.6f + EatenCell.transform.localScale.z/1.6f;
        //     this.transform.localScale = new Vector3(AddedSize,AddedSize,AddedSize);
        //     OriginalScale = this.transform.localScale;
        //     //this.GetComponent<Rigidbody>().mass = OriginalScale.z * CellManager.instance.DefaultMass;
        //     MoveSpeed = CellManager.instance.DefaultSpeed/(OriginalScale.z+CellManager.instance.SpeedDamp);
        //     Destroy(EatenCell);
        //     this.transform.DOPunchScale(this.transform.localScale*0.3f,0.4f,10,0.5f);
        // }
        // else if (this.gameObject.tag == "MotherCell")
        // {
        //     CellManager.instance.PlayerNumber --;
        //     float AddedSize = OriginalScale.z/1.6f + EatenCell.transform.localScale.z/1.6f;
        //     this.transform.localScale = new Vector3(AddedSize,AddedSize,AddedSize);
        //     OriginalScale = this.transform.localScale;
        //     //this.GetComponent<Rigidbody>().mass = OriginalScale.z * CellManager.instance.DefaultMass;
        //     MoveSpeed = CellManager.instance.DefaultSpeed/(OriginalScale.z+CellManager.instance.SpeedDamp);
        //     Destroy(EatenCell);
        //     this.transform.DOPunchScale(this.transform.localScale*0.3f,0.4f,10,0.5f);
        // }
        
    }


}
