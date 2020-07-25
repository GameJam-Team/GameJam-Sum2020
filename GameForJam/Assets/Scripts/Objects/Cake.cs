using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class Cake : MonoBehaviour
{
    private ParticleSystem Particles;
    private HealthController PlayerHealthController = null;
    private float height;
    private Vector3 dRot = new Vector3(9f / 10, 0, 0);
    private CapsuleCollider2D cakeCollider;
    private Transform cakeTransform;
    private Vector3 beginPos;
    private void Awake()
    {
        Particles = GetComponent<ParticleSystem>();
        cakeCollider = GetComponent<CapsuleCollider2D>();
        height = cakeCollider.size.y / 2f;
        cakeTransform = GetComponent<Transform>();
        beginPos = cakeTransform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealthController = collision.gameObject.GetComponent<HealthController>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.E))
        {
            Debug.Log(collision.gameObject.name);
            if (PlayerHealthController != null)
            {
                uint dHealth = PlayerHealthController.IncreaseHealth(1);
                if (dHealth != 0)
                {
                    if (!Particles.isEmitting) Particles.Play();
                    cakeTransform.Rotate(dRot);
                    cakeTransform.position = beginPos + new Vector3(0, height * (Mathf.Cos(cakeTransform.rotation.eulerAngles.x * Mathf.PI / 180) - 1), 0);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerHealthController = null;
    }
}
