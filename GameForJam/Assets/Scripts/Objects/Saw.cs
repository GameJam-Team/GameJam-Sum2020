using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    private Rigidbody2D SelfBody;
    private Transform SelfTransform;
    [SerializeField] private Vector2 _maxVelocity = new Vector2(5f, 0f);
    [SerializeField] private float _angularVelocity = 500f;
    [SerializeField] private float _reachDistance = 0.01f;
    private void Awake()
    {
        SelfBody = GetComponent<Rigidbody2D>();
        SelfTransform = GetComponent<Transform>();
    }
    private void FixedUpdate()
    {
        SelfBody.angularVelocity = _angularVelocity;
        if (Mathf.Abs(SelfTransform.localPosition.x) <= _reachDistance)
        {
            SelfBody.velocity = (SelfBody.velocity.x < 0) ? -_maxVelocity : _maxVelocity;
        }
    }
}
