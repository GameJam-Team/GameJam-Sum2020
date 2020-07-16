﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    private Rigidbody2D SelfBody;
    private Transform SelfTransform;
    private Vector2 _maxVelocity = new Vector2(5f, 0);
    private readonly float _reachDistance = 0.01f;
    private void Awake()
    {
        SelfBody = GetComponent<Rigidbody2D>();
        SelfTransform = GetComponent<Transform>();
    }
    private void FixedUpdate()
    {
        SelfBody.angularVelocity = 500f;
        if (Mathf.Abs(SelfTransform.localPosition.x) <= _reachDistance)
        {
            SelfBody.velocity = (SelfBody.velocity.x < 0) ? -_maxVelocity : _maxVelocity;
        }
    }
}
