using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawDamage : MonoBehaviour
{
    [SerializeField] private uint _damage = 50;
    private HealthController _damageable;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _damageable = collision.gameObject.GetComponent<HealthController>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_damageable != null)
            _damageable.decreaseHealth(_damage);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _damageable = null;
    }
}
