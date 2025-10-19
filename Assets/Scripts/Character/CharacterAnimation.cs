using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public Action GrenadeIsThrowing;
    public Action GrenadeHasThrowed;

    [SerializeField] Animator _animator;
    [SerializeField] CharacterMove _character;
    [SerializeField] CharacterGunsUser _characterGunsUser;

    private void OnEnable()
    {
        _character.Move += OnMove;
        _character.LeaveGround += OnJump;
        _character.GetLanded += OnLanded;
        _characterGunsUser.Armed += OnArmed;
        _characterGunsUser.DisArmed += OnDisArmed;
        _characterGunsUser.BeginGrenadeThrow += OnBeginGrenadeThrow;
    }

    private void OnDisable()
    {
        _character.Move -= OnMove;
        _character.LeaveGround -= OnJump;
        _character.GetLanded -= OnLanded;
        _characterGunsUser.Armed -= OnArmed;
        _characterGunsUser.DisArmed -= OnDisArmed;
        _characterGunsUser.BeginGrenadeThrow -= OnBeginGrenadeThrow;
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

    private void OnArmed()
    {
        _animator.SetBool("HasGun", true);
    }

    private void OnDisArmed()
    {
        _animator.SetBool("HasGun", false);
    }

    private void OnBeginGrenadeThrow()
    {
        _animator.SetBool("GrenadeLaunch", true);
    }

    private void OnGrenadeThrow()
    {
        GrenadeIsThrowing?.Invoke();
    }

    private void OnGrenadeThrowed()
    {
        _animator.SetBool("GrenadeLaunch", false);
        GrenadeHasThrowed?.Invoke();
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
