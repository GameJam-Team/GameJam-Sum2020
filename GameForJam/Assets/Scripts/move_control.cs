using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_control : MonoBehaviour
{
    [SerializeField] private float shft_distanse = 13;
    [SerializeField] private float cd_shft_time = 2;
    [SerializeField] private float x,y, tall, enviroment_speed_coef = 1, shft_cd;
    [SerializeField] private float speed = 0, jmp_speed = 0;
    private Vector3 shift;
    private CapsuleCollider2D height;
    [SerializeField] private bool down = false, jump = false, ground = true, shift_act = false, swim_mode = false;
    private Rigidbody2D _selfBody;
    private Transform _selfTransform;
    private SpriteRenderer SelfSprite;
    private readonly Stack<float> coefs = new Stack<float>();
    private HealthController HPController;
    private void Awake()
    {
        _selfBody = GetComponent<Rigidbody2D>();
        height = gameObject.GetComponent<CapsuleCollider2D>();
        tall = height.size.y;
        _selfTransform = GetComponent<Transform>();
        SelfSprite = GetComponent<SpriteRenderer>();
        coefs.Push(1);
        height = gameObject.GetComponent<CapsuleCollider2D>();
        HPController = GetComponent<HealthController>();
    }
    void Update()
    {
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey("space") && ground)
            jump = false;
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown("space")) && !jump&& ground)
        {
            jump = true;
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
        //SelfSprite.flipX = Mathf.Sign(x) < 0;
        if (Mathf.Sign(x) < 0)
        {
            _selfTransform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        else
        {
            _selfTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        if (x > 0) x = 1f;
        if (x < 0) x = -1f;
        if (Input.GetKey(KeyCode.LeftShift) && !shift_act && Mathf.Abs(x) > 0 && shft_cd <= 0 && HPController.discreaseEnergy(5)) 
        {
            shift = _selfTransform.position;
            shift_act = true;
            coefs.Push(enviroment_speed_coef);
            enviroment_speed_coef = 7;
            shft_cd = cd_shft_time;
            HPController.immortal = true;
        }
        if (shift_act)
        {
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
            else if (shft_cd<1.6) HPController.immortal = false;
        }
        if (swim_mode)
        {
            y = Input.GetAxis("Vertical");
            if ( (x != 0 || y != 0)&&HPController.discreaseEnergy(0.1f))
                _selfBody.velocity = new Vector2(_selfBody.velocity.x, y * speed * enviroment_speed_coef);
            else if (x == 0 && y == 0)
                HPController.increaseEnergy();
            else x = 0;
        }
        else HPController.increaseEnergy();
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
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Wall")&& shift_act)
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
        if (collision.gameObject.CompareTag("Ladder"))
        {
            coefs.Clear();
            coefs.Push(1);
            enviroment_speed_coef = 0.55f;
            swim_mode = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("earth"))
            ground = true;
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
        if (collision.gameObject.CompareTag("Ladder"))
        {
            enviroment_speed_coef = 1;
            swim_mode = false;
        }
    }
}
