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
    internal static BodyPart[] FindEnemyParts()
    {
        var enemyHero = GameObject.Find("EnemyHero");
        var HeroBodyparts = enemyHero.GetComponent<HeroBodyParts>();

        var bodyparts = new BodyPart[HeroBodyparts.Lenght];
        for (var i = 0; i < HeroBodyparts.Lenght; i++) //first is skipped cuz the first child is the HandAxis, not the BodyPart
        {
            bodyparts[i] = GetBodyPart(HeroBodyparts.Get(i));
        }

        return bodyparts;
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
