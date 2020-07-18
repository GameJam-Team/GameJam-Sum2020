using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cake : MonoBehaviour
{
    private ParticleSystem Particles;
    private HealthController PlayerHealth = null;

    private void Awake()
    {
        Particles = GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth = collision.gameObject.GetComponent<HealthController>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.E))
        {
            Debug.Log(collision.gameObject.name);
            if (!Particles.isEmitting) Particles.Play();
            PlayerHealth.IncreaseHealth(1);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerHealth = null;
    }
}
