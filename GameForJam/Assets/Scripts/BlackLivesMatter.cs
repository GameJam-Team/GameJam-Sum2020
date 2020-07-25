using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackLivesMatter : MonoBehaviour
{
    public GameObject Player;
    public HealthController PlayerHealth;
    public Transform PlayerTransform;
    public GameObject ExplosionPrefab;
    private GameObject[] _torches;
    private Transform SelfTransform;
    private float _activateTime;
    private float _deactivateTime;
    private float _timeUnused;
    [SerializeField] private bool pressed = false, used = false;
    private void Awake()
    {
        PlayerHealth = Player.GetComponent<HealthController>();
        PlayerTransform = Player.GetComponent<Transform>();
        SelfTransform = GetComponent<Transform>();
        _torches = new GameObject[] 
        {SelfTransform.GetChild(0).gameObject,
         SelfTransform.GetChild(1).gameObject,
         SelfTransform.GetChild(2).gameObject
        };
    }
    private void FixedUpdate()
    {
        if (PlayerHealth.TotemPressed != gameObject && pressed)
            SetTothemOff();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        _timeUnused = 4 * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Time.time - _activateTime > _timeUnused && pressed ||
                Time.time - _deactivateTime > _timeUnused && !pressed)
            {
                Debug.Log("Tothem pressed");
                if (!used && !pressed)
                {
                    SetTothemOn();
                    _activateTime = Time.time;
                }
                else if (!used)
                {
                    SetTothemOff();
                    _deactivateTime = Time.time;
                }
                else
                {
                    Instantiate(ExplosionPrefab, SelfTransform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
            }
        }
    }
    public void SetTothemOn()
    {
        pressed = true;
        PlayerHealth.TotemPressed = gameObject;
        foreach (GameObject torch in _torches)
        {
            torch.SetActive(true);
        }
    }

    public void SetTothemOff()
    {
        pressed = false;
        if (PlayerHealth.TotemPressed == gameObject)
            PlayerHealth.TotemPressed = null;
        foreach (GameObject torch in _torches)
        {
            torch.SetActive(false);
        }
    }
    public void Resurrect()
    {
        if (!used && pressed)
        {
            SetTothemOff();
            PlayerTransform.position = SelfTransform.position;
            used = true;
            Player.SetActive(true);
            PlayerHealth.IncreaseHealth(PlayerHealth.MaxHealth);
            PlayerHealth.HealthSlider.transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}
