using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndExplosion : MonoBehaviour
{
    [SerializeField] private float _explosionTime = 0.35f;
    [SerializeField] private uint _explosionDamage = 10;
    void Start()
    {
        Invoke("DelayedDestroy", _explosionTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HealthController HPController = collision.gameObject.GetComponent<HealthController>();
        if (HPController != null) HPController.decreaseHealth(_explosionDamage);
    }
    void DelayedDestroy()
    {
        Destroy(gameObject);
    }
}