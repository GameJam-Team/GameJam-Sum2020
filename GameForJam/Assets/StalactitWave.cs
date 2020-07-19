using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalactitWave : MonoBehaviour
{
    public GameObject StalactitPrefab;
    private void OnTriggerExit2D(Collider2D collision)
    {
        var a = Random.Range(1, 1000);
        if ((a & 1) == 1)
        {
            Instantiate(StalactitPrefab, transform.position, Quaternion.identity);
        }
    }
}
