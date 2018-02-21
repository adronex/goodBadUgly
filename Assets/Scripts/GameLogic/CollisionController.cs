using UnityEngine;

public static class CollisionController
{
    #region Public methods
    public static int CheckCollision(Vector2 bulletPos, BodyPart[] bodyParts)
    {
        for (int i = 0; i < bodyParts.Length; i++)
        {
            var points = GetRealPoints(bodyParts[i]);

            var min = float.MaxValue;
            var max = float.MinValue;
            int minId = 0;
            int maxId = 0;

            for (int j = 0; j < points.Length; j++)
            {
                var dist = Vector2.Distance(points[j], bulletPos);
                if (dist < min)
                {
                    min = dist;
                    minId = j;
                }
                else if (dist > max)
                {
                    max = dist;
                    maxId = j;
                }
            }

            var possiblePoints = GetPossiblePoints(points, minId, maxId);

            if (IsBelongToHeroPart(bulletPos, possiblePoints))
            {
                return Random.Range(bodyParts[i].MinDamage, bodyParts[i].MaxDamage);
            }
        }
        return -1;
    }
    #endregion

    #region Private methods
    private static Vector2[] GetRealPoints(BodyPart bodyPart)
    {
        var pos = bodyPart.Transform.position;

        return new Vector2[4]
            {
                new Vector2(pos.x - bodyPart.Width, pos.y - bodyPart.Height),
                new Vector2(pos.x + bodyPart.Width, pos.y - bodyPart.Height),
                new Vector2(pos.x + bodyPart.Width, pos.y + bodyPart.Height),
                new Vector2(pos.x - bodyPart.Width, pos.y + bodyPart.Height),
            };
    }


    private static Vector2[] GetPossiblePoints(Vector2[] realPoints, int minId, int maxId)
    {
        Vector2[] points = new Vector2[6];
        bool isRevert = true;
        for (int i = 0, j = 0; j < 6; i++, j++)
        {
            var direction = realPoints[i] - (Vector2)GameManager.ownGunPoint.position;
            var offset = direction.normalized * Bullet.Speed;
            if (i == minId)
            {
                points[j] = realPoints[i];
                continue;
            }
            else if (i == maxId)
            {
                points[j] = realPoints[i] + offset;
                continue;
            }

            if (isRevert)
            {
                points[j] = realPoints[i];
                points[j + 1] = realPoints[i] + offset;
                isRevert = false;
                j++;
            }
            else
            {
                points[j] = realPoints[i] + offset;
                points[j + 1] = realPoints[i];
                j++;
            }
        }
        return points;
    }


    //https://ru.wikibooks.org/wiki/Реализации_алгоритмов/Задача_о_принадлежности_точки_многоугольнику
    private static bool IsBelongToHeroPart(Vector2 point, Vector2[] p) //p is the points
    {
        int previus = p.Length - 1;
        bool isBelong = false;

        for (int current = 0; current < p.Length; current++)
        {
            if (p[current].y < point.y && p[previus].y >= point.y || p[previus].y < point.y && p[current].y >= point.y)
            {
                if (p[current].x + (point.y - p[current].y) / (p[previus].y - p[current].y) * (p[previus].x - p[current].x) < point.x)
                {
                    isBelong = !isBelong;
                }
            }
            previus = current;
        }
        return isBelong;
    }
    #endregion
}
