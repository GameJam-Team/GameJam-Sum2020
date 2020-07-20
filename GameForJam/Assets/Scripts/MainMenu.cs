using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string _startSceneName;
    private void Awake()
    {
        _startSceneName = SceneManager.GetActiveScene().name;
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        SceneManager.LoadScene(_startSceneName);
        SceneManager.LoadScene("inventory", LoadSceneMode.Additive);
    }
}
