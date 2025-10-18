using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour, IMover
{
    [SerializeField] Animator _animator;
    [SerializeField] GameObject _jumperCharacter;

    private IJumper _jumper;

    public void Move(Vector2 direction)
    {
        _animator.SetInteger("movez", FloatToInt(direction.x));
        _animator.SetInteger("movex", FloatToInt(direction.y));
    }

    public void Look(Vector2 direction)
    {

    }

    private void Awake()
    {
        if(_jumperCharacter.TryGetComponent<IJumper>(out _jumper))
        {
            _jumper.LeaveGround += OnJump;
            _jumper.GetLanded += OnLanded;
        }
    }

    private void OnJump()
    {
        _animator.SetBool("isJumping", true);
    }

    private void OnLanded()
    {
        _animator.SetBool("isJumping", false);
    }

    private int FloatToInt(float value)
    {
        if (value == 0) 
            return 0;
        else if (value < 0)
            return -1;
        else
            return 1;
    }
}
