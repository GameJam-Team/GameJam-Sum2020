using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBodyScript : MonoBehaviour
{
    void Start()
    {
        Invoke("DestroyUselessBody", 60f);
    }
    void DestroyUselessBody()
    {
        Destroy(gameObject);
    }
}
