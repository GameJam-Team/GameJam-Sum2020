using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    public GameObject Player;
    public void ChangeScene(GameObject curSceneObject, GameObject transitSceneObject)
    {
        Player.transform.SetParent(transitSceneObject.transform);
        curSceneObject.SetActive(false);
        transitSceneObject.SetActive(true);
    }
}
