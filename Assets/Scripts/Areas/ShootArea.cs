using UnityEngine;

public class ShootArea : Area
{
    public static event ShootAreaPressedEventHandler ShootAreaPressedEvent;
    

    public override void Pressed()
    {
        if (ShootAreaPressedEvent != null)
        {
            ShootAreaPressedEvent();
        }
    }


    public ShootArea(RectTransform transformZone) : base(transformZone)
    {
    }


    public delegate void ShootAreaPressedEventHandler();
}
