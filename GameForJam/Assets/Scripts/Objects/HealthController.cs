using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private uint _health;
    private ParticleSystem _particleSystem;
    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }
    public void increaseHealth(uint value)
    {
        _health += value;
    }
    public void decreaseHealth(uint value)
    {
        _health -= (value > _health)? _health : value;
        if (!_particleSystem.isEmitting) _particleSystem.Play();
        if (_health == 0)
        {
            Destroy(gameObject);
        }
    }
}
