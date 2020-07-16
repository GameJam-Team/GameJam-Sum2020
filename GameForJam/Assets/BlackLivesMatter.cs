using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackLivesMatter : MonoBehaviour
{
    public HealthController PlayerHealth;
    public Transform PlayerTransform;
    private Transform[] _torches;
    private Transform SelfTransform;
    private bool pressed = false, used = false;
    private void Awake()
    {
        SelfTransform = GetComponent<Transform>();
        _torches = new  Transform[] 
        {SelfTransform.GetChild(0),
         SelfTransform.GetChild(1),
         SelfTransform.GetChild(2)
        };
    }
    private void FixedUpdate()
    {
        if (PlayerHealth.Health == 0)
        {
            Resurrect();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!pressed)
        {
            pressed = true;
            PlayerHealth.resurrectable = true;
            foreach (var torch in _torches)
            {
                torch.gameObject.SetActive(true);
            }
        }
    }

    public void Resurrect()
    {
        if (!used && pressed)
        {
            PlayerTransform.position = transform.position;
            used = true;
            PlayerTransform.gameObject.SetActive(true);
            PlayerHealth.Health = PlayerHealth.MaxHealth;
            foreach (var torch in _torches)
            {
                torch.gameObject.SetActive(false);
            }
            PlayerHealth.HealthSlider.transform.GetChild(1).gameObject.SetActive(true);
            PlayerHealth.resurrectable = false;
            PlayerHealth.HealthSlider.value = (float)PlayerHealth.Health / PlayerHealth.MaxHealth * 100;
        }
    }
}
