using UnityEngine;

public class AimArea : Area
{
    public static event AimAreaPressedEventHandler AimAreaPressedEvent;
    public static event AimAreaDepressedEventHandler AimAreaDepressedEvent;
    

    public override void Pressed()
    {
        if (AimAreaPressedEvent != null)
        {
            AimAreaPressedEvent();
        }
    }


    public override void Depressed()
    {
        if (AimAreaDepressedEvent != null)
        {
            AimAreaDepressedEvent();
        }
    }


    public AimArea(RectTransform transformZone) : base(transformZone)
    {
    }


    public delegate void AimAreaPressedEventHandler();
    public delegate void AimAreaDepressedEventHandler();
}
