using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam_control : MonoBehaviour
{
    public float x, y, offset;
    Vector2 direction;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        x = Input.GetAxis("Debug Horizontal");
        y = Input.GetAxis("Debug Vertical");
        if (x == 0 && y == 0)
        {
            direction = Vector2.zero - gameObject.GetComponent<Camera>().lensShift;
            gameObject.GetComponent<Camera>().lensShift += direction * 0.03f;
        }
        else
        {
            if (Mathf.Abs(gameObject.GetComponent<Camera>().lensShift.x + x * 0.01f) > offset)
                x = 0;
            if (Mathf.Abs(gameObject.GetComponent<Camera>().lensShift.y + y * 0.01f) > offset)
                y = 0;
            direction = new Vector2(x, y);
            gameObject.GetComponent<Camera>().lensShift += direction * 0.01f;
        }
    }
}
