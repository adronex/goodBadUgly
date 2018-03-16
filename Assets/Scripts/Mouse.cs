public class Mouse
{
    private bool isLookingToMouse;
    public bool IsLooking { get { return isLookingToMouse; } }

    public void StartLook()
    {
        if (!isLookingToMouse)
        {
            isLookingToMouse = true;
        }
    }


    public void StopLook()
    {
        if (isLookingToMouse)
        {
            isLookingToMouse = false;
        }
    }
}