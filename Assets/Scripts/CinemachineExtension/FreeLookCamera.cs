using Cinemachine;

public class FreeLookCamera : CinemachineFreeLook
{
    public void SetXValue(float value)
    {
        m_XAxis.Value = value;
    }
}