using UI;
using UnityEngine;

namespace Core
{
    public class HitBox
    {
        #region Properties
        internal Transform Transform { get; private set; }

        internal float HalfWidth { get; private set; }

        internal float HalfHeight { get; private set; }

        internal int Damage { get; private set; }
        #endregion
        #region Public methods
        internal static HitBox[] GetHitBox(Transform hitbox)
        {
            var hitboxParts = new HitBox[hitbox.childCount];
            var hitboxLength = hitboxParts.Length;
            Transform hitboxPart;
            for (int index = 0; index < hitboxLength; index++)
            {
                hitboxPart = hitbox.GetChild(index);
                hitboxParts[index] = GetBodyPart(hitboxPart);
            }

            return hitboxParts;
        }
        #endregion
        #region Private methods
        private static HitBox GetBodyPart(Transform bodyPart)
        {
            var spriteRender = bodyPart.GetComponent<SpriteRenderer>();
            var spriteSize = spriteRender.size;

            var info = bodyPart.GetComponent<BodyPartInfo>();

            return new HitBox
            {
                Transform = bodyPart,
                HalfWidth = spriteSize.x * bodyPart.lossyScale.x / 2,
                HalfHeight = spriteSize.y * bodyPart.lossyScale.y / 2,
                Damage = info.Damage
            };
        }
        #endregion
    }
}