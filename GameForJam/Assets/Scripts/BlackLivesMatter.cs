using System.Collections;
using System.Collections.Generic;
using UnityEditor.Android;
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
        if (PlayerHealth.TotemPressed != gameObject && pressed)
            SetTothemOff();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Tothem pressed");
            if (!used && !pressed)
                SetTothemOn();
            else if (!used) 
                SetTothemOff();
            else
            {
                Instantiate(Explosion, gameObject.transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    public void SetTothemOn()
    {
        pressed = true;
        PlayerHealth.TotemPressed = gameObject;
        foreach (var torch in _torches)
        {
            torch.gameObject.SetActive(true);
        }
    }

    public void SetTothemOff()
    {
        pressed = false;
        if (PlayerHealth.TotemPressed == gameObject)
            PlayerHealth.TotemPressed = null;
        foreach (var torch in _torches)
        {
            torch.gameObject.SetActive(false);
        }
    }
    public void Resurrect()
    {
        if (!used && pressed)
        {
            SetTothemOff();
            PlayerTransform.position = transform.position;
            used = true;
            PlayerTransform.gameObject.SetActive(true);
            PlayerHealth.Health = PlayerHealth.MaxHealth;
            PlayerHealth.HealthSlider.transform.GetChild(1).gameObject.SetActive(true);
            PlayerHealth.HealthSlider.value = (float)PlayerHealth.Health / PlayerHealth.MaxHealth * 100;
        }
    }
}
