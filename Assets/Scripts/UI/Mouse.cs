public class Mouse
{
    public bool IsLooking { get; private set; }

    public void StartLook()
    {
        if (!IsLooking)
        {
            IsLooking = true;
        }
    }


    public void StopLook()
    {
        if (IsLooking)
        {
            IsLooking = false;
        }
    }
}