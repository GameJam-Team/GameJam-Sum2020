using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private bool _isFire = true;
    private Transform _selfTransform;
    private GameObject _fireGO;
    private void Awake()
    {
        _selfTransform = GetComponent<Transform>();
        _fireGO = _selfTransform.GetChild(0).gameObject;
    }
    private void OnMouseDown()
    {
        _isFire = !_isFire;
        _fireGO.SetActive(_isFire);
    }
}
