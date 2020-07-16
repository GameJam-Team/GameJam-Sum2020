using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
   public bool immortal;
    [SerializeField] private uint _health = 555, _maxHealth = 555;
        public float _oxigen, _energy;
    [SerializeField] private Slider _healthSlider;

    private ParticleSystem _particleSystem;
    private void Awake()
    {
        immortal = false;
        _healthSlider.value = 100;
        _particleSystem = GetComponent<ParticleSystem>();
    }
    public void decreaseHealth(uint value)
    {
        if (!immortal)
        {
            _health -= (value > _health) ? _health : value;
            _healthSlider.value = (float)_health / _maxHealth * 100;
            if (!_particleSystem.isEmitting) _particleSystem.Play();
            if (_health == 0)
            {
                _healthSlider.transform.GetChild(1).gameObject.SetActive(false);
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
        _oxigen -= (_oxigen < 0.4f) ? _oxigen : 0.4f;
        if (_oxigen <= 0.01f) decreaseHealth(1);
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
