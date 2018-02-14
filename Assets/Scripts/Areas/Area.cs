using UnityEngine;

public abstract class Area
{
    protected float MinX;
    protected float MinY;
    protected float MaxX;
    protected float MaxY;


    public abstract void Pressed();
    public virtual void Depressed()
    {
        //Nothing
    }

    
    public bool Check(Vector2 mousePos)
    {
        if (mousePos.x >= MinX && mousePos.x <= MaxX)
        {
            if (mousePos.y >= MinY && mousePos.y <= MaxY)
            {
                return true;
            }
        }

        return false;
    }


    public Area(RectTransform transformZone)
    {
        var pos = transformZone.position;
        var rect = transformZone.rect;

        var offsetMin = transformZone.offsetMin;
        MinX = pos.x + rect.xMin;
        MinY = pos.y + rect.yMin;

        var offsetMax = transformZone.offsetMax;
        MaxX = pos.x + rect.xMax;
        MaxY = pos.y + rect.yMax;
    }
}
