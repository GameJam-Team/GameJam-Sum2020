using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_control : MonoBehaviour
{
    public  float shft_distanse = 13;
    public  float cd_shft_time = 2;
    private float x,y, tall, enviroment_speed_coef = 1, shft_cd;
    public float speed, jmp_speed;
    private Vector3 shift;
    private CapsuleCollider2D height;
    private bool down = false, jump = false, ground = true, shift_act = false, swim_mode = false;
    private Rigidbody2D _selfBody;
    private Transform _selfTransform;
    private SpriteRenderer SelfSprite;
    private readonly Stack<float> coefs = new Stack<float>();
    private HealthController HP;
    private void Awake()
    {
        _selfBody = GetComponent<Rigidbody2D>();
        height = gameObject.GetComponent<CapsuleCollider2D>();
        tall = height.size.y;
        _selfTransform = GetComponent<Transform>();
        SelfSprite = GetComponent<SpriteRenderer>();
        coefs.Push(1);
        height = gameObject.GetComponent<CapsuleCollider2D>();
        HP = this.GetComponent<HealthController>();
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
            _selfBody.AddForce(Vector2.up * jmp_speed, ForceMode2D.Impulse);
        }
        if (!swim_mode)
        {
            if (ground && !jump && Input.GetKey(KeyCode.S) && !down)
            {
                height.size = new Vector2(height.size.x, tall / 2f);
                coefs.Push(enviroment_speed_coef);
                enviroment_speed_coef = 0.35f;
                down = true;
            }

            if ((Input.GetKeyUp(KeyCode.S) || !ground) && down)
            {
                height.size = new Vector2(height.size.x, tall);
                // if (Input.GetKeyUp(KeyCode.S)&& ground)
                enviroment_speed_coef = coefs.Pop();
                down = false;
            }
        }
        else if (down)
        {
            height.size = new Vector2(height.size.x, tall);
           
            down = false;
        }
    }


    private void FixedUpdate()
    {
        x = Input.GetAxis("Horizontal");
        SelfSprite.flipX = Mathf.Sign(x) < 0;
        if (x > 0) x = 1f;
        if (x < 0) x = -1f;
        if (Input.GetKey(KeyCode.LeftShift) && !shift_act && Mathf.Abs(x) > 0 && shft_cd <= 0 && HP.discreaseEnergy(5)) 
        {
            shift = _selfTransform.position;
            shift_act = true;
            coefs.Push(enviroment_speed_coef);
            enviroment_speed_coef = 7;
            //y= x = 1* Mathf.Sign(x);
            shft_cd = cd_shft_time;
            HP.immortal = true;
            
            //this.GetComponent<Rigidbody2D>().AddForce(Vector2.right * speed * x * enviroment_speed_coef, ForceMode2D.Impulse);
        }

        if (shift_act)
        {
            // x = y;
            if (Vector3.Distance(shift, _selfTransform.position) > shft_distanse)
            {
                shift_act = false;
                
                enviroment_speed_coef = coefs.Pop();
            }
        }

        if (shft_cd > 0)
        {
            shft_cd -= Time.deltaTime;
            if (shft_cd >= 1.6 && !shift_act)
                x = 0;
            else if (shft_cd<1.6) HP.immortal = false;

        }
        //direction = new Vector3(x, 0, 0);
        // if (!shift_act)
        if (swim_mode)
        {

            y = Input.GetAxis("Vertical");
            if ( (x != 0 || y != 0)&&HP.discreaseEnergy(0.1f))
                _selfBody.velocity = new Vector2(_selfBody.velocity.x, y * speed * enviroment_speed_coef);
            else if (x == 0 && y == 0)
                HP.increaseEnergy();
            else x = 0;
        }
        else HP.increaseEnergy();
      
        _selfBody.velocity = new Vector2(x * speed * enviroment_speed_coef, _selfBody.velocity.y);
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("earth")) && shift_act)
        {
            shift_act = false;
            enviroment_speed_coef = 1;
            
        }
        
    }
   void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("earth"))
         ground = true;
        if (collision.gameObject.CompareTag("water"))
        {
            coefs.Clear();
            coefs.Push(1);
            enviroment_speed_coef = 0.5f;
            swim_mode = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("earth"))
            ground = false;
        if (collision.gameObject.CompareTag("water"))
        {

            enviroment_speed_coef = 1;
            swim_mode = false;
        }
    }
}
