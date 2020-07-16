using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : InteractiveObject
{
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public override void ButtonInteraction()
    {
        Debug.Log("Button has Pressed by button");
    }
    public override void ArmInteraction()
    {
        _animator.Play("ButtonPressed");
        Debug.Log("Button has Pressed by Arm");
    }
}
