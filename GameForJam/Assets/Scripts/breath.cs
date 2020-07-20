using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breath : MonoBehaviour
{
    [SerializeField] private bool air = true;
    private HealthController HPController;
    void Awake()
    {
        HPController = GetComponentInParent<HealthController>();
    }
    void Update()
    {
        if (air) HPController.increaseOxigen();
        else HPController.discreaseOxygen();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("water"))
            air = false;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("water"))
            air = true;
    }
}
