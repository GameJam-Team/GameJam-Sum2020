using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesCollision : MonoBehaviour
{
    [SerializeField] private uint _damage = 20;
    private HealthController _damageable;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        _damageable = collision.gameObject.GetComponent<HealthController>();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        _damageable.decreaseHealth(_damage);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        _damageable = null;
    }
}
