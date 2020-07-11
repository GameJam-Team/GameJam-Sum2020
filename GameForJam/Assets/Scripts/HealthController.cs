using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
   public bool immortal;
    [SerializeField] private uint _health;
        public float _oxigen, _energy;

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
    public void increaseOxigen()
    {if (_oxigen < 100) 
        _oxigen += 0.5f;
    }
    public void discreaseOxygen()
    {
        _oxigen -= 0.4f;
        if (_oxigen <= 0) Destroy(gameObject);
    }
    public void increaseEnergy()
    {if (_energy < 100) 
        _energy += 0.01f;
    }
    public bool discreaseEnergy ( float decrement)
    {
        if (_energy > decrement)
        {
            _energy -= decrement;
            return true;
        }
        else return false;
    }
}
