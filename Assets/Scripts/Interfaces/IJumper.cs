using System;

public interface IJumper
{
    Action LeaveGround { get; set; }
    Action GetLanded { get; set; }
    public void Jump();
}
