using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalagmitDamager : MonoBehaviour
{
    private Vector3 _beginPos;
    private Vector3 _collisionPos;
    private uint _damage;
    private uint _unitDamage = 5;
    private void Awake()
    {
        _beginPos = transform.position;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        _collisionPos = collision.transform.position;
        if (collision.gameObject.CompareTag("Player"))
        {
            _damage = (uint)(_collisionPos.y - _beginPos.y) * _unitDamage;
            HealthController damageable = collision.gameObject.GetComponent<HealthController>();
            damageable.decreaseHealth(_damage);
        }
        Invoke("DestroyStalactitUnit", 0.5f);
    }
    private void DestroyStalactitUnit()
    {
        Destroy(gameObject);
    }
}
