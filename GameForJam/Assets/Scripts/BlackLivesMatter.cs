using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackLivesMatter : MonoBehaviour
{
    public HealthController PlayerHealth;
    public Transform PlayerTransform;
    public GameObject Explosion;
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
            if (!used) Resurrect();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {   
            if (!used && !pressed)
            {
                pressed = true;
                PlayerHealth.TotemPressed++;
                foreach (var torch in _torches)
                {
                    torch.gameObject.SetActive(true);
                }
            }
            else if (!used) 
            {
                pressed = false;
                PlayerHealth.TotemPressed--;
                foreach (var torch in _torches)
                {
                    torch.gameObject.SetActive(false);
                }
            }
            else
            {
                Instantiate(Explosion, gameObject.transform.position, Quaternion.identity);
                Destroy(gameObject);
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
            PlayerHealth.TotemPressed --;
            PlayerHealth.HealthSlider.value = (float)PlayerHealth.Health / PlayerHealth.MaxHealth * 100;
        }
    }
}
