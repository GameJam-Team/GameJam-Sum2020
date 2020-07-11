using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public Transform Player;
    public float dampTime = 0.4f;
    private Vector3 cameraPos;
    private Vector3 velocity = Vector3.zero;
    private Transform _camTransform;
    private void Awake()
    {
        _camTransform = GetComponent<Transform>();
    }
    void Update()
    {
        if (Player != null)
        {
            cameraPos = new Vector3(Player.position.x, Player.position.y, -10f);
            _camTransform.position = Vector3.SmoothDamp(_camTransform.position, cameraPos, ref velocity, dampTime);
        }
    }
}