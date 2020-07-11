using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam_control : MonoBehaviour
{
    public float x, y, offset;
    private Vector2 direction;
    public Camera _mainCamera;
    private void Awake()
    {
       // _mainCamera = Camera.main;
    }
    private void FixedUpdate()
    {
        x = Input.GetAxis("Debug Horizontal");
        y = Input.GetAxis("Debug Vertical");
        if (x == 0 && y == 0)
        {
            direction = Vector2.zero - _mainCamera.lensShift;
            _mainCamera.lensShift += direction * 0.03f;
        }
        else
        {
            if (Mathf.Abs(_mainCamera.lensShift.x + x * 0.01f) > offset)
                x = 0;
            if (Mathf.Abs(_mainCamera.lensShift.y + y * 0.01f) > offset)
                y = 0;
            direction = new Vector2(x, y);
            _mainCamera.lensShift += direction * 0.01f;
        }
    }
}