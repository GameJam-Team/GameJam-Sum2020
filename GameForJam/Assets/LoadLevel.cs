using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public MainMenu start;
    public void LoadScene()
    {
        string sceneName = PlayerPrefs.GetString("level");
        start._startSceneName = sceneName;
        start.StartGame();
    }
}
