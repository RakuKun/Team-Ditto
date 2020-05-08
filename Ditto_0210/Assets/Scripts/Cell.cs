using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    
    
    public GameObject CellObject;
    public Vector3 OriginalScale;
    Vector3 previous;
    public Rigidbody rb;
    float JoyStickA;
    float JoyStickA_y;
    public string joystick;
    public float MaxSpeed = 8;
  
    public bool isGrounded = true;
    public float JumpPower = 3500;
    public float JumpHeight = 1.5f;
    public float JumpDistance = 1;
    public float MoveSpeed = 5;
    public float MoveForce = 80;
    public GameObject SecondCamera;
    public int points = 0;
    public bool isFirstJump = false;
    public Text Instructiontext;
    private Collision EatenOne;
    private bool EatenCollision = false;
    public Vector3 currentVel;
    public GameObject Expression;
    public ParticleSystem MagEffect;
    
    
    // Start is called before the first frame update
    private void Awake() {
         JumpHeight = CellManager.instance.DefaultJumpHeight;
         JumpPower = CellManager.instance.DefaultJumpPower;
         MoveForce = CellManager.instance.DefaultMoveForce;
         MaxSpeed = CellManager.instance.DefaultMaxSpeed;
    }
    void Start()
    {
        OriginalScale = this.transform.localScale;
        rb = this.GetComponent<Rigidbody>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        JoyStickA = Input.GetAxisRaw(joystick);
        JoyStickA_y = Input.GetAxisRaw(joystick+"y");
        

        //Key input for testing
        if (Input.GetKey("a") && isGrounded)
        {
            transform.Translate(Vector3.left*Time.deltaTime*MoveSpeed,Space.World);
        }
        if (Input.GetKey("d") && isGrounded)
        {
            transform.Translate(Vector3.right*Time.deltaTime*MoveSpeed,Space.World);
        }
       
        
        //split
        // if (Input.GetKeyDown(joystick + " button 2"))
        // {
        //     Split();
        // }
        
        //change scale
        
    
        
        
        Stretching();

        if (Input.GetKeyDown(joystick + " button 0")  || Input.GetKeyDown("space"))
        {
            //IsGrounded();
            if (isGrounded)
            {
                //isFirstJump = true;
                
                 
                //FinishJump = false;
                //rb.useGravity = false;
                //isGrounded = false;
                StartCoroutine(Jump(Vector2.one));
            }
            else if (isFirstJump)
            {
                isFirstJump = false;
                StartCoroutine(Jump(Vector2.one));
            }
            
        }
        if (Input.GetKeyDown(joystick + " button 1"))
        {

           MagEffect.Play();
     
        }
        if (Input.GetKeyUp(joystick + " button 1"))
        {
             MagEffect.Stop();
        }

        //isGrounded = IsGround();
        
    }

    private bool IsGround()
    {
        if (this.transform.GetChild(0).GetComponent<DetectGround>().isGrounded == false
            && this.transform.GetChild(1).GetComponent<DetectGround>().isGrounded == false)
            return false;
        else 
            return true;
    }

    private void FixedUpdate() 
    {
        if (Input.GetAxisRaw(joystick)!= 0 )
        {
            //if (isGrounded)
                LazyFollow(MoveForce);
            //else
                //LazyFollow(MoveForce*1f);
        }
        //Stretching();
        if(rb.velocity.magnitude > MaxSpeed)
        {
            rb.velocity = rb.velocity.normalized * MaxSpeed;
        } 
        //IsGrounded();
        
    }
    

    

    public void Stretching()
    {
        if (this.gameObject.layer != 11)
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
        currentVel =  new Vector3(this.transform.localScale.x, this.transform.localScale.y,this.transform.localScale.z);                                      
        previous = this.transform.position;
        }
    }
    

    public void LazyFollow(float MoveForce)
    {
        rb.AddForce(Vector3.right*JoyStickA*MoveForce);
        rb.AddForce(-Vector3.forward*JoyStickA_y*MoveForce);
        //transform.Translate(Vector3.right*Time.deltaTime*JoyStickA*MoveSpeed,Space.World);     
    }

    public IEnumerator Jump(Vector3 direction)
    {
        
        // if (Input.GetAxisRaw(joystick) > 0)
        //     direction = new Vector3(JumpDistance,0,JumpHeight);
        // else if (Input.GetAxisRaw(joystick) < 0)
        //     direction = new Vector3(-JumpDistance,0,JumpHeight);
        // else 
        //     direction = new Vector3(0,JumpHeight,0);
        if (JoyStickA!=0)
        {
            direction = new Vector3(JoyStickA,0,0);
            rb.AddForce(direction *JumpPower);
        }
        if (JoyStickA_y!=0)
        {
            direction = new Vector3(0,0,-JoyStickA_y);
            rb.AddForce(direction *JumpPower);
        }
        yield return new WaitForSeconds(0.3f);

    }

    public void Split()
    {
        if (this.transform.localScale.x > CellManager.instance.SmallestCellSize 
            && this.transform.localScale.y > CellManager.instance.SmallestCellSize 
            && this.transform.localScale.z > CellManager.instance.SmallestCellSize)
        {
            CellManager.instance.PlayerNumber ++;
            GameObject newCell = Instantiate(CellObject, this.transform.position,this.transform.rotation);
            newCell.GetComponent<Cell>().joystick = "joystick " + CellManager.instance.PlayerNumber.ToString();
            newCell.GetComponent<Cell>().points = 0;
            //this.transform.localScale = OriginalScale*0.8f;
            OriginalScale = this.transform.localScale;
            newCell.transform.localScale = OriginalScale;
            newCell.GetComponent<Cell>().OriginalScale = this.transform.localScale;
            //newCell.GetComponent<Rigidbody>().useGravity = true;
            //MoveSpeed = CellManager.instance.DefaultSpeed/(OriginalScale.z+CellManager.instance.SpeedDamp);
            //JumpHeight = CellManager.instance.DefaultJumpHeight * this.transform.localScale.z;
            //JumpPower = CellManager.instance.DefaultJumpPower * this.transform.localScale.z;
            //rb.mass = CellManager.instance.DefaultMass * this.transform.localScale.z;
            newCell.GetComponent<Rigidbody>().mass = CellManager.instance.DefaultMass;// * this.transform.localScale.z;
            newCell.GetComponent<Cell>().JumpPower = CellManager.instance.DefaultJumpPower;// * this.transform.localScale.z;
            newCell.GetComponent<Cell>().JumpHeight = CellManager.instance.DefaultJumpHeight;// * this.transform.localScale.z;
            newCell.GetComponent<Cell>().MoveSpeed = CellManager.instance.DefaultSpeed;///(newCell.transform.localScale.z+CellManager.instance.SpeedDamp);
            this.transform.DOPunchScale(this.transform.localScale*-0.3f,0.4f,10,0.5f);
            if (SecondCamera!=null && SecondCamera.GetComponent<FollowPlayer>().Player == null)
            {
                SecondCamera.GetComponent<FollowPlayer>().Player = newCell;
                Instructiontext.text = "Welcome!";
                StartCoroutine(SetupWelcome());
            }
        }
    }

    IEnumerator SetupWelcome()
    {
        yield return new WaitForSeconds(1.5f);
        Instructiontext.text = "";
    }

    private void OnCollisionStay(Collision other) 
    {
        if (other.gameObject.tag == "Virus")
        {
            //EatenOne = other;
            //EatenCollision = true;
            if (Input.GetKey(joystick + " button 3"))
            {
                
                Merge(other.gameObject);
            }
        }

        if (other.gameObject.tag == "Lymph")
        {
            if (Input.GetKeyDown(joystick + " button 2"))
            {
                //Split();
                StateShareMgr.instance.OpenUI();
            }
        }
        
    }

    
    

    // private void OnCollisionExit(Collision other) 
    // {
    //     if (other.gameObject.tag == "Cell")
    //     {
    //         EatenOne = other;
    //         EatenCollision = false;
    //     }
    // }
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Coin")
        {
            Debug.Log("Coin");
            this.transform.DOPunchScale(this.transform.localScale*0.2f,0.2f,10,0.5f);
            Destroy(other.gameObject);
            Expression.GetComponent<Animator>().SetBool("suprised",true);
            points ++;
            if (this.gameObject.tag == "MotherCell")
            {
                CellManager.instance.TotalPoints = points;
                CellManager.instance.UpdateMainScore();
            }
            else if (joystick == "joystick 2")
            {
                CellManager.instance.SecondPoints = points;
                CellManager.instance.UpdateSecondScore();
            }
        }
    }

    

    protected void Merge(GameObject EatenCell)
    {
        // if (this.transform.localScale.x > EatenCell.transform.localScale.x && this.gameObject.tag != "Virus")   
        // { 
        //     CellManager.instance.PlayerNumber --;
        //     float AddedSize = OriginalScale.z/1.6f + EatenCell.transform.localScale.z/1.6f;
        //     this.transform.localScale = new Vector3(AddedSize,AddedSize,AddedSize);
        //     OriginalScale = this.transform.localScale;
        //     MoveSpeed = CellManager.instance.DefaultSpeed/(OriginalScale.z+CellManager.instance.SpeedDamp);
        //     points += EatenCell.GetComponent<Cell>().points;
            
        //     Destroy(EatenCell);
        //     this.transform.DOPunchScale(this.transform.localScale*0.3f,0.4f,5,0.2f);
        //     JumpHeight = CellManager.instance.DefaultJumpHeight * this.transform.localScale.z;
        //     JumpPower = CellManager.instance.DefaultJumpPower * this.transform.localScale.z ;
        //     rb.mass = CellManager.instance.DefaultMass * this.transform.localScale.z;
        //     if (joystick == "joystick 2")
        //     {
        //         CellManager.instance.SecondPoints = points;
        //         CellManager.instance.UpdateSecondScore();
        //     }
            
        // }
        if (this.gameObject.tag != "Virus")
        {
            //CellManager.instance.PlayerNumber --;
            //float AddedSize = OriginalScale.z/1.6f + EatenCell.transform.localScale.z/1.6f;
            //this.transform.localScale = new Vector3(AddedSize,AddedSize,AddedSize);
            //OriginalScale = this.transform.localScale;
            //MoveSpeed = CellManager.instance.DefaultSpeed/(OriginalScale.z+CellManager.instance.SpeedDamp);
            //points += EatenCell.GetComponent<Cell>().points;
            // if (EatenCell.GetComponent<Cell>().joystick == "joystick 2")
            // {
            //     //Instructiontext.text = "Thank you for playing!";
            //     CellManager.instance.SecondPoints = 0;
            //     CellManager.instance.UpdateSecondScore();
            // }
            StartCoroutine(DestroyVirus(EatenCell));
            this.transform.DOPunchScale(this.transform.localScale*0.02f,0.4f,5,0.2f);
            
            //JumpHeight = CellManager.instance.DefaultJumpHeight * this.transform.localScale.z;
            //JumpPower = CellManager.instance.DefaultJumpPower * this.transform.localScale.z;
            //rb.mass = CellManager.instance.DefaultMass * this.transform.localScale.z;
            Debug.Log(points);
            //CellManager.instance.TotalPoints = points;
            //CellManager.instance.UpdateMainScore();
        }
        
    }

    IEnumerator DestroyVirus(GameObject EatenCell)
    {
        EatenCell.GetComponent<Animator>().SetBool("eat",true);
        EatenCell.transform.GetChild(0).GetChild(0).gameObject.GetComponent<VirusFaceControl>().Eaten();
        yield return new WaitForSeconds(0.4f);
        Destroy(EatenCell);
    }

    public void MagneticPower()
    {

    }


}
