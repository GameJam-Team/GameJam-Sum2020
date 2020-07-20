using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public GameObject TransitLocation;
    private GameObject _curLocation;
    public SceneChanger sceneChanger;

    private void Awake()
    {
        _curLocation = transform.parent.gameObject;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Scene changed");
            sceneChanger.ChangeScene(_curLocation, TransitLocation);
        }
    }
}
