using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] CharacterMove _character;

    private void OnEnable()
    {
        _character.Move += OnMove;
        _character.LeaveGround += OnJump;
        _character.GetLanded += OnLanded;
    }

    private void OnDisable()
    {
        _character.Move -= OnMove;
        _character.LeaveGround -= OnJump;
        _character.GetLanded -= OnLanded;
    }

    private void OnMove(Vector2 direction)
    {
        _animator.SetInteger("movez", FloatToInt(direction.x));
        _animator.SetInteger("movex", FloatToInt(direction.y));
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
