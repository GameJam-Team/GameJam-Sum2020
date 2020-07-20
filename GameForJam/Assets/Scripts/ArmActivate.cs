﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering;

public class ArmActivate : MonoBehaviour
{
    public InteractiveObject Object;
    private Transform SelfTransform;
    private bool _armActivated = false;
    private float _activateTime;
    private float _deactivateTime;
    private float _timeUnused;
    [SerializeField] private Vector3 _eulers = new Vector3(0, 0, 60.37f);
    private void Awake()
    {
        SelfTransform = transform.GetChild(0);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        _timeUnused = 4 * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Time.time - _activateTime > _timeUnused && _armActivated ||
                Time.time - _deactivateTime > _timeUnused && !_armActivated)
            {
                _armActivated = !_armActivated;
                if (_armActivated)
                {
                    Debug.Log("Rotation");
                    SelfTransform.Rotate(_eulers);
                    Object.ArmInteraction();
                    _activateTime = Time.time;
                }
                else
                {
                    Debug.Log("IrRotation");
                    SelfTransform.Rotate(-_eulers);
                    _deactivateTime = Time.time;
                }
            }
        }
    }
}
