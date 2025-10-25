using System;
using UnityEngine;

public interface IWalker
{
    Action<Vector2> Move { get; set; }
}