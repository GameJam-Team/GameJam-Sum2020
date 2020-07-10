using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_control : MonoBehaviour
{
    public float shft_distanse = 13;
    public float cd_shft_time = 2;
	public float speed;
	public float jmp_speed;    

    private float x, y;
	private float tall;
	private float enviroment_speed_coef; // MB public? ... Never mind
	private float shft_cd;
    
    private Vector3 direction;
	private Vector3 shift;

    private CapsuleCollider2D height;
    private bool jump;
	private bool ground
	private bool shift_act;	// It could be name like 'toShift'

    void Start()
    {
        enviroment_speed_coef = 1f;
        height = gameObject.GetComponent<CapsuleCollider2D>();	// height? Capsule??
        tall = height.size.y;	// tall...
        jump = false;
        ground = true;	// are you shure about that? 
        shift_act = false;
    }


    void Update()
    {
        /*if (jump && ground && Input.GetAxis("Vertical") <= 0)
            jump = false;*/
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey("space") && ground)	// dose this actually need?
            // if (jump && ground /*&& Input.GetAxis("Vertical") <= 0*/)
            jump = false;
        if ((Input.GetKeyDown(KeyCode.W)|| Input.GetKeyDown("space")) && !jump)
        {
            jump = true;
            ground = false;
            this.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jmp_speed, ForceMode2D.Impulse);	// GetComponent is very time consuming operation
																									// It'd be better to store RigidBody2D in a private var
            
        }


        if (Input.GetKeyDown(KeyCode.S) && !jump && ground)
        {
            height.size = new Vector2(height.size.x, tall / 1.75f);	// Constant ???
            enviroment_speed_coef = 0.19f;
        }


        if (Input.GetKeyUp(KeyCode.S) || !ground)
        {
            height.size = new Vector2(height.size.x, tall);
            if (Input.GetKeyUp(KeyCode.S))
                enviroment_speed_coef = 1f;
        }
        
    }
           
   
    private void FixedUpdate()
    {
       
        x = Input.GetAxis("Horizontal");
        
        if (Input.GetKey(KeyCode.LeftShift) && !shift_act&& Mathf.Abs(x) > 0 && shft_cd <= 0)
        {
            shift = this.transform.position;
            shift_act = true;
            enviroment_speed_coef = 10f;
            y = x = 1* Mathf.Sign(x);	// 1 * Mathf.Sign ??
            shft_cd = cd_shft_time;		// not obviously
        }
        if (shift_act)
        {
            x = y;
            if (Vector3.Distance(shift, this.transform.position) > shft_distanse) // mb you can use here Vector2 to calculate a distance
            {
                shift_act = false;
                enviroment_speed_coef = 1f;
            }
        }
        if (shft_cd > 0f)
            shft_cd -= Time.deltaTime;
            
        direction = new Vector3(x, 0f, 0f);
        this.transform.position = this.transform.position + direction * speed * enviroment_speed_coef;             
    }
               
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "earth")	// it's better to make a tag field to chose which tags are 'ground'
													// it also could be done via layers
           ground = true;

    }
}
