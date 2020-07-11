using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D SelfBody;
    [SerializeField][Range(1,100)] private float JumpForce, MoveSpeed;
    private SpriteRenderer SelfSprite;

    private void Awake()
    {
        SelfBody = GetComponent<Rigidbody2D>();
        SelfSprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Vector2 velocity = SelfBody.velocity;
        if (Input.GetKey(KeyCode.D))
        {
            velocity.x = MoveSpeed;
            SelfSprite.flipX = false;
        }
        if (Input.GetKey(KeyCode.A))
        {
            velocity.x = -MoveSpeed;
            SelfSprite.flipX = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = JumpForce;
        }
        SelfBody.velocity = velocity;
    }
}
