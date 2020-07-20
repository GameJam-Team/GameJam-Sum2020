using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    public Transform PlayerTransform;
    public void ChangeScene(GameObject curSceneObject, GameObject transitSceneObject)
    {
        PlayerTransform.SetParent(transitSceneObject.transform);
        curSceneObject.SetActive(false);
        transitSceneObject.SetActive(true);
    }
}
