using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_control : MonoBehaviour
{
    public  float shft_distanse = 13;
    public  float cd_shft_time = 2;
    //public int y;
    float y, x,tall,enviroment_speed_coef,shft_cd;
    public float speed, jmp_speed;
    Vector3 direction, shift;
    // Start is called before the first frame update
    CapsuleCollider2D height;
     bool jump, ground,shift_act;
    void Start()
    {
        enviroment_speed_coef = 1;
        height = gameObject.GetComponent<CapsuleCollider2D>();
        tall = height.size.y;
        jump = false;
        ground = true;
        shift_act = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (jump && ground && Input.GetAxis("Vertical") <= 0)
            jump = false;*/
        if (!Input.GetKey(KeyCode.W)&& !Input.GetKey("space") && ground)
            // if (jump && ground /*&& Input.GetAxis("Vertical") <= 0*/)
            jump = false;
        if ((Input.GetKeyDown(KeyCode.W)|| Input.GetKeyDown("space")) && !jump)
        {
            jump = true;
            ground = false;
            this.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jmp_speed, ForceMode2D.Impulse);
            
        }


        if (Input.GetKeyDown(KeyCode.S) && !jump && ground)
        {
            height.size = new Vector2(height.size.x, tall / 1.75f);
            enviroment_speed_coef = 0.19f;
        }


        if (Input.GetKeyUp(KeyCode.S) || !ground)
        {
            height.size = new Vector2(height.size.x, tall);
            if (Input.GetKeyUp(KeyCode.S))
                enviroment_speed_coef = 1;
        }
        
    }
           
    

    private void FixedUpdate()
    {
       
        x = Input.GetAxis("Horizontal");
        
        if (Input.GetKey(KeyCode.LeftShift) && !shift_act&& Mathf.Abs( x)>0&& shft_cd<=0)
        {
            shift = this.transform.position;
            shift_act = true;
            enviroment_speed_coef = 10;
            y= x = 1* Mathf.Sign(x);
            shft_cd = cd_shft_time;
        }
        if (shift_act)
        {
            x = y;
            if (Vector3.Distance(shift, this.transform.position) > shft_distanse)
            {
                shift_act = false;
                enviroment_speed_coef = 1;
            }
        }
        if (shft_cd > 0)
            shft_cd -= Time.deltaTime;
            
        direction = new Vector3(x, 0, 0);
        this.transform.position = this.transform.position + direction * speed* enviroment_speed_coef;             
    }
               
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "earth")
           ground = true;

    }
}
