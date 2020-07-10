using System;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private Collider2D l_trigger;
    [SerializeField] private Transform p_transform;
    [SerializeField] private bool state = false;
    
    private Animator anim;
    public event EventHandler<bool> LeverEvent;

    private void Awake()
    {
        if (p_transform == null)
            Debug.LogWarning($"{gameObject} doesn't attach to a Player");

        anim = GetComponentInChildren<Animator>();
        Animate(state);
    }

    private void Update()
    {
        if (l_trigger.OverlapPoint(p_transform.position) && Input.GetButtonDown("Submit"))
        {
            state = !state;
            Animate(state);
            LeverEvent?.Invoke(gameObject, state);
        }
    }

    private void Animate(bool state)
    {
        anim.SetBool("turn_off", state);
    }
}
