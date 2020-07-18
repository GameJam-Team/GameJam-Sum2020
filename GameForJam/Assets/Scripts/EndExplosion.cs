using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndExplosion : MonoBehaviour
{
    void Start()
    {
        Invoke("DelayedDestory", 0.35f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<HealthController>().decreaseHealth(10);
    }
    void DelayedDestory()
    {
        Destroy(gameObject);
    }
}