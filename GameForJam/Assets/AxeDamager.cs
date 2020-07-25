using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeDamager : MonoBehaviour
{
    private Animator _animator;
    private Collider2D collisionEnemy = null;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _animator.SetTrigger("Hit");
            if (collisionEnemy != null) 
            {
                var CC = collisionEnemy.GetComponent<Enemy>();
                if (CC != null)
                {
                    CC.decreaseHealth(10);
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision Enter");
        collisionEnemy = collision;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        collisionEnemy = null;
    }
}
