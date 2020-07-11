using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breath : MonoBehaviour
{
    private bool air = true;
   private HealthController HP;
    // Start is called before the first frame update
    void Start()
    {
        HP = GetComponentInParent<HealthController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (air) HP.increaseOxigen();
        else HP.discreaseOxygen();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.CompareTag("water"))
        {
            
            air = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("water"))

            air = true;
    }
}
