using UnityEngine;

public class BodyPart
{
    #region Properties
    internal Transform Transform { get; private set; }

    internal float HalfWidth { get; private set; }

    internal float HalfHeight { get; private set; }

    internal int Damage { get; private set; }
    #endregion

    #region Public methods
    internal static BodyPart[] FindEnemyParts(Transform areas)
    {
        Transform currentAreas = GetCurrentAreas(areas);

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
    private static Transform GetCurrentAreas(Transform areas)
    {
        Transform currentArea = null;
        for (int i = 0; i < areas.childCount; i++)
        {
            var area = areas.GetChild(i);
            if (area.gameObject.activeSelf)
            {
                currentArea = area;
                break;
            }
        }

        return currentArea;
    }


    private static BodyPart GetBodyPart(Transform bodyPart)
    {
        var spriteRender = bodyPart.GetComponent<SpriteRenderer>();
        var spriteSize = spriteRender.size;

        var info = bodyPart.GetComponent<BodyPartInfo>();

        return new BodyPart
        {
            Transform = bodyPart,
            HalfWidth = spriteSize.x * bodyPart.lossyScale.x / 2,
            HalfHeight = spriteSize.y * bodyPart.lossyScale.y / 2,
            Damage = info.Damage
        };
    }
    #endregion
}
