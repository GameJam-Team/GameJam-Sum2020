using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public Canvas canvas;
    private GameObject GameOverTextObj;
    private GameObject RestartButton;

    private void Awake()
    {
        GameOverTextObj = canvas.transform.GetChild(1).gameObject;
        RestartButton = canvas.transform.GetChild(2).gameObject;
    }

    public void GameOver()
    {
        GameOverTextObj.SetActive(true);
        RestartButton.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
