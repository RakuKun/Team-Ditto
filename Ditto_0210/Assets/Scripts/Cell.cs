using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cell : MonoBehaviour
{
    
    
    public GameObject CellObject;
    public Vector3 OriginalScale;
    Vector3 previous;
    public Rigidbody rb;
    float JoyStickA;
    public string joystick;
    public float MaxSpeed = 8;
  
    public bool isGrounded = true;
    public int JumpPower = 3500;
    public float JumpHeight = 1.5f;
    public float JumpDistance = 1;
    public float MoveSpeed = 5;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        OriginalScale = this.transform.localScale;
        rb = this.GetComponent<Rigidbody>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        JoyStickA = Input.GetAxisRaw(joystick);
        
        if (Input.GetAxisRaw(joystick)!= 0 && isGrounded)
        {
            LazyFollow(this.transform.position + Vector3.right * Time.deltaTime*JoyStickA*40);
        }

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
        if (Input.GetKeyDown(joystick + " button 2"))
        {
            Split();
        }
        
        //change scale
        Stretching();
        
    
        if (Input.GetKeyDown(joystick + " button 0")  || Input.GetKeyDown("space"))
        {
            IsGrounded();
            if (isGrounded)
            {
                isGrounded = false;
                //FinishJump = false;
                //rb.useGravity = false;
            
                StartCoroutine(Jump(Vector2.one));
            }
            
        }
        
        
    }

    private void FixedUpdate() 
    {
        if(rb.velocity.magnitude > MaxSpeed)
        {
            rb.velocity = rb.velocity.normalized * MaxSpeed;
        } 
    }
    void IsGrounded()
    {
        if (Physics.Raycast(transform.position, -Vector3.up, this.transform.localScale.y/2 + 0.05f))
        {
            isGrounded = true;
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
        rb.AddForce(direction *JumpPower);
        yield return new WaitForSeconds(0.1f);
    }

    protected void Split()
    {
        if (this.transform.localScale.x>0.4f && this.transform.localScale.y>0.4f &&this.transform.localScale.z>0.4f)
        {
            CellManager.instance.PlayerNumber ++;
            GameObject newCell = Instantiate(CellObject, this.transform.position,this.transform.rotation);
            newCell.GetComponent<Cell>().joystick = "joystick " + CellManager.instance.PlayerNumber.ToString();
            this.transform.localScale = OriginalScale*0.8f;
            OriginalScale = this.transform.localScale;
            newCell.transform.localScale = OriginalScale;
            newCell.GetComponent<Cell>().OriginalScale = this.transform.localScale;
            newCell.GetComponent<Rigidbody>().useGravity = true;
            MoveSpeed = CellManager.instance.DefaultSpeed/(OriginalScale.z+CellManager.instance.SpeedDamp);
            newCell.GetComponent<Cell>().MoveSpeed = CellManager.instance.DefaultSpeed/(newCell.transform.localScale.z+CellManager.instance.SpeedDamp);
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.tag == "Cell")
        {
            if (Input.GetKey(joystick + " button 3"))
            {
                Merge(other.gameObject);
            }
        }
        IsGrounded();
        
    }

    

    protected void Merge(GameObject EatenCell)
    {
        if (this.transform.localScale.x > EatenCell.transform.localScale.x)   
        { 
            CellManager.instance.PlayerNumber --;
            float AddedSize = OriginalScale.z/1.6f + EatenCell.transform.localScale.z/1.6f;
            this.transform.localScale = new Vector3(AddedSize,AddedSize,AddedSize);
            OriginalScale = this.transform.localScale;
            MoveSpeed = CellManager.instance.DefaultSpeed/(OriginalScale.z+CellManager.instance.SpeedDamp);
            Destroy(EatenCell);
            this.transform.DOPunchScale(this.transform.localScale*0.3f,0.4f,10,0.5f);
        }
        else if (this.gameObject.tag == "MotherCell")
        {
            CellManager.instance.PlayerNumber --;
            float AddedSize = OriginalScale.z/1.6f + EatenCell.transform.localScale.z/1.6f;
            this.transform.localScale = new Vector3(AddedSize,AddedSize,AddedSize);
            OriginalScale = this.transform.localScale;
            MoveSpeed = CellManager.instance.DefaultSpeed/(OriginalScale.z+CellManager.instance.SpeedDamp);
            Destroy(EatenCell);
            this.transform.DOPunchScale(this.transform.localScale*0.3f,0.4f,10,0.5f);
        }
        
    }


}
