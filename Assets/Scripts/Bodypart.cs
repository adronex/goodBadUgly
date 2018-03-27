using UnityEngine;

public class BodyPart
{
    #region Properties
    internal Transform Transform { get; private set; }

    internal float Width { get; private set; }

    internal float Height { get; private set; }

    internal int Damage { get; private set; }
    #endregion

    #region Public methods
    internal static BodyPart[] FindEnemyParts()//todo
    {
        var allAreas = GameObject.Find("EnemyCowboy").transform.Find("Areas");

        Transform currentAreas = null;
        for (int i = 0; i < allAreas.childCount; i++)
        {
            var areas = allAreas.GetChild(i);
            if (areas.gameObject.activeSelf)
            {
                currentAreas = areas;
                break;
            }
        }

        var bodyParts = new BodyPart[currentAreas.childCount];
        for (int i = 0; i < bodyParts.Length; i++)
        {
            var area = currentAreas.GetChild(i);
            bodyParts[i] = GetBodyPart(area);
        }
         
        return bodyParts;
    }
    #endregion

    #region Private methods
    private static BodyPart GetBodyPart(Transform bodyPart)
    {
        var spriteSize = bodyPart.GetComponent<SpriteRenderer>().size;
        var info = bodyPart.GetComponent<BodyPartInfo>();

        return new BodyPart
        {
            Transform = bodyPart,
            Width = spriteSize.x * bodyPart.lossyScale.x / 2,
            Height = spriteSize.y * bodyPart.lossyScale.y / 2,
            Damage = info.Damage
        };
    }
    #endregion
}
