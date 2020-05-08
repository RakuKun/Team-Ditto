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
       
        
        Stretching();

        //Jump
        if (Input.GetKeyDown(joystick + " button 0")  || Input.GetKeyDown("space"))
        {
            //IsGrounded();
            if (isGrounded)
            {

                StartCoroutine(Jump(Vector2.one));
            }
            else if (isFirstJump)
            {
                isFirstJump = false;
                StartCoroutine(Jump(Vector2.one));
            }
            
        }
        //Magnetic power
        if (Input.GetKeyDown(joystick + " button 1"))
        {

           MagEffect.Play();
     
        }
        if (Input.GetKeyUp(joystick + " button 1"))
        {
             MagEffect.Stop();
        }

        
    }

    //detect if the cell is grounded
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
                LazyFollow(MoveForce);
        }
        if(rb.velocity.magnitude > MaxSpeed)
        {
            rb.velocity = rb.velocity.normalized * MaxSpeed;
        } 
        
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
       
    }

    public IEnumerator Jump(Vector3 direction)
    {
        
        
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

    //Cell duplication
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
        //detect virus
        if (other.gameObject.tag == "Virus")
        {
            //EatenOne = other;
            //EatenCollision = true;
            if (Input.GetKey(joystick + " button 3"))
            {
                
                Merge(other.gameObject);
            }
        }

        //detect lymph node
        if (other.gameObject.tag == "Lymph")
        {
            if (Input.GetKeyDown(joystick + " button 2"))
            {
                //Split();
                StateShareMgr.instance.OpenUI();
            }
        }
        
    }

 
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

    
    //Merge,eat virus
    protected void Merge(GameObject EatenCell)
    {
       
        if (this.gameObject.tag != "Virus")
        {
           
            StartCoroutine(DestroyVirus(EatenCell));
            this.transform.DOPunchScale(this.transform.localScale*0.02f,0.4f,5,0.2f);
           
            Debug.Log(points);
            
        }
        
    }

    IEnumerator DestroyVirus(GameObject EatenCell)
    {
        EatenCell.GetComponent<Animator>().SetBool("eat",true);
        EatenCell.transform.GetChild(0).GetChild(0).gameObject.GetComponent<VirusFaceControl>().Eaten();
        yield return new WaitForSeconds(0.4f);
        Destroy(EatenCell);
    }

 


}
