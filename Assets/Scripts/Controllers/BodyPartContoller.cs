using UnityEngine;
public class BodyPartContoller
{
    public Vector2[] GetPoints(BodyPartModel bodyPart)
    {
        var pos = bodyPart.Transform.position;

        return new Vector2[4]
            {
                new Vector2(pos.x - bodyPart.Width, pos.y - bodyPart.Height),
                new Vector2(pos.x + bodyPart.Width, pos.y - bodyPart.Height),
                new Vector2(pos.x - bodyPart.Width, pos.y + bodyPart.Height),
                new Vector2(pos.x + bodyPart.Width, pos.y + bodyPart.Height),
            };
    }

    public void FindEnemyParts()
    {
        var enemy = GameObject.Find("EnemyHero").transform;
        var bodyparts = new BodyPartModel[enemy.childCount];
        for (int i = 0; i < enemy.childCount; i++)
        {
            bodyparts[i] = GetBodyPart(enemy.GetChild(i).transform);
        }

        App.Model.EnemyHero.BodyParts = bodyparts;
    }

    private BodyPartModel GetBodyPart(Transform bodyPart)
    {
        var spriteSize = bodyPart.GetComponent<SpriteRenderer>().size;
        var info = bodyPart.GetComponent<BodyPartInfo>();

        return new BodyPartModel
        {
            Transform = bodyPart,
            Width = spriteSize.x * bodyPart.lossyScale.x / 2,
            Height = spriteSize.y * bodyPart.lossyScale.y / 2,
            MinDamage = info.MinDamage,
            MaxDamage = info.MaxDamage
        };
    }
}
