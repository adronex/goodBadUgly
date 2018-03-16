using UnityEngine;

namespace Model
{
    public class BodyPart
    {
        #region Fields
        private Transform transform;
        private float width;
        private float height;
        private int damage;
        #endregion

        #region Properties
        internal Transform Transform
        {
            get { return transform; }
        }

        internal float Width
        {
            get { return width; }
        }

        internal float Height
        {
            get { return height; }
        }

        internal int Damage
        {
            get { return damage; }
        }
        #endregion

        #region Public methods
        internal static BodyPart[] FindEnemyParts()
        {
            var enemyHero = GameObject.Find("EnemyHero");
            var HeroBodyparts = enemyHero.GetComponent<HeroBodyParts>();

            var bodyparts = new BodyPart[HeroBodyparts.Lenght];
            for (int i = 0; i < HeroBodyparts.Lenght; i++) //first is skipped cuz the first child is the HandAxis, not the BodyPart
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
                transform = bodyPart,
                width = spriteSize.x * bodyPart.lossyScale.x / 2,
                height = spriteSize.y * bodyPart.lossyScale.y / 2,
                damage = info.Damage
            };
        }
        #endregion
    }
}
