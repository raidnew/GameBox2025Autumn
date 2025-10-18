using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour, IMover
{
    [SerializeField] Animator _animator;

    public void Move(Vector2 direction)
    {
        _animator.SetInteger("movez", FloatToInt(direction.x));
        _animator.SetInteger("movex", FloatToInt(direction.y));
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
