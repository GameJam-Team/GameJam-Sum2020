using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public bool immortal;
    [SerializeField] private uint Health = 100;
    public uint MaxHealth = 100;
    public float _oxigen, _energy;
    public GameObject TotemPressed = null;
    public Slider HealthSlider;
    public GameState MainScene;
    private Transform _playerTransform;
    private GameObject _curSceneObject;
    private ParticleSystem _particleSystem;
    private void Awake()
    {
        _playerTransform = GetComponent<Transform>();
        _curSceneObject = _playerTransform.parent.gameObject;
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
                if (TotemPressed == null)
                    MainScene.GameOver();
                else
                {
                    if (TotemPressed.transform.parent != gameObject.transform.parent)
                    {
                        _curSceneObject.SetActive(false);
                        Transform newSceneTransform = TotemPressed.transform.parent;
                        newSceneTransform.gameObject.SetActive(true);
                        _playerTransform.parent = newSceneTransform;
                    }
                    TotemPressed.GetComponent<BlackLivesMatter>().Resurrect();
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
    {
        if (_energy < 100) 
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
