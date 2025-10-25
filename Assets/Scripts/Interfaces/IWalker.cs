using System;
using UnityEngine;

public interface IWalker
{
    Action<Vector2> Move { get; set; }
    Action LeaveGround { get; set; }
    Action GetLanded { get; set; }
}