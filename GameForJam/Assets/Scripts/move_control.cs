using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_control : MonoBehaviour
{
    public  float shft_distanse = 13;
    public  float cd_shft_time = 2;
    //public int y;
    float x,tall,enviroment_speed_coef,shft_cd;
    public float speed, jmp_speed;
    Vector3 shift;
    // Start is called before the first frame update
    CapsuleCollider2D height;
     bool down,jump, ground,shift_act;
    Rigidbody2D phis;
    Stack <float> coefs = new Stack<float>();
    void Start()
    {
        coefs.Push(1);
        enviroment_speed_coef = 1;
        height = gameObject.GetComponent<CapsuleCollider2D>();
        tall = height.size.y;
        jump = false;
        ground = true;
        shift_act = false;
        down = false;
        phis = this.GetComponent<Rigidbody2D>();
    }

 
    void Update()
    {
        /*if (jump && ground && Input.GetAxis("Vertical") <= 0)
            jump = false;*/
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey("space") && ground)
            // if (jump && ground /*&& Input.GetAxis("Vertical") <= 0*/)
            jump = false;
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown("space")) && !jump&& ground)
        {
            jump = true;
           // ground = false;
            phis.AddForce(Vector2.up * jmp_speed, ForceMode2D.Impulse);

        }
        

        if ( ground&& !jump &&Input.GetKey(KeyCode.S) && !down)
        {
            height.size = new Vector2(height.size.x, tall / 2f);
            coefs.Push(enviroment_speed_coef);
            enviroment_speed_coef = 0.35f;
            down = true;

        }


        if ((Input.GetKeyUp(KeyCode.S) || !ground)&&down)
        {
            height.size = new Vector2(height.size.x, tall);
           // if (Input.GetKeyUp(KeyCode.S)&& ground)
                enviroment_speed_coef = coefs.Pop();
            down = false;
        }

    }
           
    

    private void FixedUpdate()
    {
        

        x = Input.GetAxis("Horizontal");
        if (x > 0) x = 1f;
        if (x < 0) x = -1f;
        if (Input.GetKey(KeyCode.LeftShift) && !shift_act&& Mathf.Abs( x)>0&& shft_cd<=0)
        {
            shift = this.transform.position;
            shift_act = true;
            coefs.Push(enviroment_speed_coef);
            enviroment_speed_coef = 15;
            //y= x = 1* Mathf.Sign(x);
            shft_cd = cd_shft_time;
            //this.GetComponent<Rigidbody2D>().AddForce(Vector2.right * speed * x * enviroment_speed_coef, ForceMode2D.Impulse);
        }
        if (shift_act)
        {
           // x = y;
            
            if (Vector3.Distance(shift, this.transform.position) > shft_distanse)
            {
                shift_act = false;
                enviroment_speed_coef = coefs.Pop();    
            }
        }
        if (shft_cd > 0)
            shft_cd -= Time.deltaTime;

        //direction = new Vector3(x, 0, 0);
       // if (!shift_act)
        phis.velocity = new Vector2(x * speed * enviroment_speed_coef, phis.velocity.y);           
    }
               
    void OnCollisionEnter2D(Collision2D collision)
    {
       
        if ((collision.gameObject.tag == "Wall"|| collision.gameObject.tag == "earth") && shift_act)
        {
            shift_act = false;
            enviroment_speed_coef = 1;
        }
    }
   void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "earth")
         ground = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "earth")
            ground = false;
        Debug.Log("Fff");
    }
}
