using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalactitWave : MonoBehaviour
{
    public GameObject StalactitPrefab;
    private Transform SelfTransform;
    private void Awake()
    {
        SelfTransform = GetComponent<Transform>();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((Random.Range(1, 11) & 1) == 1)
            Instantiate(StalactitPrefab, SelfTransform.position, Quaternion.identity);
    }
}
