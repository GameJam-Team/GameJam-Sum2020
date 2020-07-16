using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private bool _isFire = true;
    private Transform _selfTransform;
    private GameObject _fire;
    private void Awake()
    {
        _selfTransform = GetComponent<Transform>();
        _fire = _selfTransform.GetChild(0).gameObject;
    }
    private void OnMouseDown()
    {
        _isFire = !_isFire;
        _fire.SetActive(_isFire);
    }
}
