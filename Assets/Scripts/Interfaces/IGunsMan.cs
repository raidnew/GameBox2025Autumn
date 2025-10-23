using System;

public interface IGunsMan
{
    public Action Armed { get; set; }
    public Action DisArmed { get; set; }
    public Action BeginGrenadeThrow { get; set; }
}