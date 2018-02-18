using UnityEngine;
public class BulletController
{
    const int size = 4;

    public bool IsBelongToHeroPart(Vector2 bulletPos, Vector2[] p)
    {
        int s = size - 1;
        bool isBelong = false;

        for (int k = 0; k < size; k++)
        {
            if ((p[k].y < bulletPos.y && p[s].y >= bulletPos.y || p[s].y < bulletPos.y && p[k].y >= bulletPos.y) &&
                 (p[k].x + (bulletPos.y - p[k].y) / (p[s].y - p[k].y) * (p[s].x - p[k].x) < bulletPos.x))
                isBelong = !isBelong;
            s = k;
        }
        return isBelong;
    }
}