using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class WinPlatform : MonoBehaviour
{
    public GameObject bigStalactitPrefab;
    private GameObject Player;
    public string nextLevelName;
    public MainMenu mainMenu;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void ChangeScene()
    {
        mainMenu._startSceneName = nextLevelName;
        PlayerPrefs.SetString("level", nextLevelName);
        mainMenu.StartGame();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Player)
        {
            GameObject stalactit = Instantiate(bigStalactitPrefab, Player.transform.position + new Vector3(0, 10, 0), Quaternion.identity);
            stalactit.transform.localScale = new Vector3(10, 10, 1);
            stalactit.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 5f));
            Invoke("ChangeScene", 1f);
        }
    }
}
