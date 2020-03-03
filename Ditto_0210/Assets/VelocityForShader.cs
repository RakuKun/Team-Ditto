using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class VelocityForShader : MonoBehaviour
{

    public Cell cell;
    string joystick;

    public float offsetX, offsetY;

    Vector3 dir;

    Rigidbody rigidbody;

    // public Material jump_mat;
    // public Material rest_mat;

    public Texture jump_tex;
    public Texture rest_tex;
    private Transform camera;
    private Matrix4x4 oldWorldMatrix, worldMatrix;
     private MeshRenderer meshRender;
     private Mesh mesh;


     
    // Start is called before the first frame update
    void Start()
    {
        if (rigidbody!= null)
         rigidbody = this.GetComponent<Rigidbody>();

         joystick = cell.joystick;
         camera = Camera.main.transform;
         meshRender = GetComponent<MeshRenderer>();
         mesh = GetComponent<Mesh>();
         oldWorldMatrix = Matrix4x4.zero;
         worldMatrix = Matrix4x4.zero;
        meshRender.sharedMaterials[0].SetTexture("_MainTex", rest_tex);
    }

    // Update is called once per frame
    void Update()
    {
        if (rigidbody!= null)
        {textureTheBall(rigidbody, meshRender,joystick);
        
        }else{
            textureThePlane(meshRender, joystick, offsetX, offsetY);
        }


         
    }
    void textureThePlane( MeshRenderer meshRender, string joystick, float offsetX, float offsety){
       // dir = rigidbody.velocity;
        //transform.LookAt(camera, Vector3.up);
        //transform.Rotate(90,0,0);
        meshRender.sharedMaterial.SetMatrix("_OldWorldMatrix", oldWorldMatrix);
         worldMatrix = transform.localToWorldMatrix;
         meshRender.sharedMaterial.SetMatrix("_WorldMatrix", worldMatrix);
         oldWorldMatrix = worldMatrix;
        //Debug.Log(dir);



         if (Input.GetAxisRaw(joystick) < 0){
             meshRender.sharedMaterial.SetTextureOffset("_BaseMap", new Vector2(offsetX-0.06f, offsetY));
         }

                  if (Input.GetAxisRaw(joystick) > 0){
             meshRender.sharedMaterial.SetTextureOffset("_BaseMap", new Vector2(-offsetX - 0.02f , offsetY));
         }


         if ((Input.GetKeyDown(joystick + " button 0")|| Input.GetKeyDown("space"))){
                
           // meshRender.material = jump_mat;
           meshRender.sharedMaterial.SetTexture("_BaseMap", jump_tex);
           //meshRender.sharedMaterial.SetTextureOffset("_MainTex", new Vector2(0.08f, -0.12f));
              
         }else{
             if (cell.isGrounded)
               // meshRender.material = rest_mat;
               
               meshRender.sharedMaterial.SetTexture("_BaseMap", rest_tex);
         }
    }

    void textureTheBall(Rigidbody rigidbody, MeshRenderer meshRender, string joystick){
       // dir = rigidbody.velocity;
        transform.LookAt(camera, Vector3.up);
        //transform.Rotate(90,0,0);
        meshRender.sharedMaterial.SetMatrix("_OldWorldMatrix", oldWorldMatrix);
         worldMatrix = transform.localToWorldMatrix;
         meshRender.sharedMaterial.SetMatrix("_WorldMatrix", worldMatrix);
         oldWorldMatrix = worldMatrix;
        //Debug.Log(dir);



         if (Input.GetAxisRaw(joystick) < 0){
             meshRender.sharedMaterial.SetTextureOffset("_MainTex", new Vector2(0.08f, -0.12f));
         }

                  if (Input.GetAxisRaw(joystick) > 0){
             meshRender.sharedMaterial.SetTextureOffset("_MainTex", new Vector2(-0.08f, -0.12f));
         }


         if ((Input.GetKeyDown(joystick + " button 0")|| Input.GetKeyDown("space"))){
                
           // meshRender.material = jump_mat;
           meshRender.sharedMaterial.SetTexture("_MainTex", jump_tex);
           //meshRender.sharedMaterial.SetTextureOffset("_MainTex", new Vector2(0.08f, -0.12f));
              
         }else{
             if (cell.isGrounded)
               // meshRender.material = rest_mat;
               
               meshRender.sharedMaterial.SetTexture("_MainTex", rest_tex);
         }
    }
}
