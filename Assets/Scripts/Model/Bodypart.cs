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
        public Transform Transform
        {
            get { return transform; }
        }

        public float Width
        {
            get { return width; }
        }

        public float Height
        {
            get { return height; }
        }

        public int Damage
        {
            get { return damage; }
        }
        #endregion

        #region Public methods
        public static BodyPart[] FindEnemyParts()
        {
            var transform = GameObject.Find("EnemyHero").transform;
            var bodyparts = new BodyPart[transform.childCount - 1];
            for (int i = 0; i < transform.childCount - 1; i++) //first is skipped cuz the first child is the HandAxis, not the BodyPart
            {
                bodyparts[i] = GetBodyPart(transform.GetChild(i + 1).transform);
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
