using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public uint Health = 100;
    public uint maxHealth = 100;
    private GameObject Player;
    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector3(10f * Mathf.Cos(Time.time), 0, 0);
        if (GetComponent<Rigidbody2D>().velocity.x >= 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == Player)
            DamagePlayer(10);
    }
    public void DamagePlayer(uint value)
    {
        Player.GetComponent<HealthController>().decreaseHealth(value);
        Player.GetComponent<Rigidbody2D>().freezeRotation = false;
        Player.transform.Rotate(new Vector3(0, 0, 60f));
        Invoke("StabilePlayer", 1f);
    }
    public void StabilePlayer()
    {
        Player.GetComponent<Rigidbody2D>().freezeRotation = true;
    }
    public void decreaseHealth(uint value)
    {
        Health -= (value > Health) ? Health : value;
        if (Health == 0)
        {
            Destroy(gameObject);
        }
    }
}
