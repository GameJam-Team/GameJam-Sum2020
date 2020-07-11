using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressButton : MonoBehaviour
{
    public InteractiveObject Object;
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _animator.SetBool("ButtonPressed", true);
            Object.ButtonInteraction();
        }
        else
        {
            _animator.SetBool("ButtonPressed", false);
        }
    }
}
