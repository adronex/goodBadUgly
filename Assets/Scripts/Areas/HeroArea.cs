using UnityEngine;

public class HeroArea : Area
{
    public static event HeroAreaPressedEventHandler HeroAreaPressedEvent;
    public static event HeroAreaDepressedEventHandler HeroAreaDepressedEvent;
    

    public override void Pressed()
    {
        if (HeroAreaPressedEvent != null)
        {
            HeroAreaPressedEvent();
        }
    }


    public override void Depressed()
    {
        if (HeroAreaDepressedEvent != null)
        {
            HeroAreaDepressedEvent();
        }
    }


    public HeroArea(RectTransform transformZone) : base(transformZone)
    {
    }


    public delegate void HeroAreaPressedEventHandler();
    public delegate void HeroAreaDepressedEventHandler();
}