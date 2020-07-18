using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public bool immortal;
    public uint Health = 555, MaxHealth = 555;
    public float _oxigen, _energy;
    public uint TotemPressed = 0;
    public Slider HealthSlider;
    public GameState MainScene;

    private ParticleSystem _particleSystem;
    private void Awake()
    {
        immortal = false;
        HealthSlider.value = 100;
        _particleSystem = GetComponent<ParticleSystem>();
    }
    public void decreaseHealth(uint value)
    {
        if (!immortal)
        {
            Health -= (value > Health) ? Health : value;
            HealthSlider.value = (float)Health / MaxHealth * 100;
            if (!_particleSystem.isEmitting) _particleSystem.Play();
            if (Health == 0)
            {
                HealthSlider.transform.GetChild(1).gameObject.SetActive(false);
                gameObject.SetActive(false);
                if (TotemPressed == 0)
                {
                    MainScene.GameOver();
                }
            }
        }
    }
    public void IncreaseHealth(uint value)
    {
        Health += (value < MaxHealth - Health) ? value : MaxHealth - Health;
        HealthSlider.value = (float)Health / MaxHealth * 100;
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
