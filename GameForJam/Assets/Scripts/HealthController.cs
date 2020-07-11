using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
   public bool immortal;
    [SerializeField] private uint _health;
    private ParticleSystem _particleSystem;
    private void Awake()
    {
        immortal = false;
        _particleSystem = GetComponent<ParticleSystem>();
    }
    public void increaseHealth(uint value)
    {
        _health += value;
    }
    public void decreaseHealth(uint value)
    {
        if (!immortal)
        {
            _health -= (value > _health) ? _health : value;
            if (!_particleSystem.isEmitting) _particleSystem.Play();
            if (_health == 0)
            {
                Destroy(gameObject);
            }
        }
        
    }
}
