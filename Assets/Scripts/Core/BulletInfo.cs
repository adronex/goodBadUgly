using UnityEngine;

namespace Core
{
    public class BulletInfo
    {
        #region Fields
        private int id;
        private Vector2 direction;
        private float speed;
        private Vector2 startPosition;
        #endregion
        #region Properties
        public Vector2 CurrentPosition { get; private set; }

        public Vector2 PreviousPosition { get; private set; }


        public float ImpactAngle
        {
            get { return Vector2.Angle(startPosition, CurrentPosition); }
        }
        #endregion
        #region Public Methods
        public BulletInfo(int bulletId, Vector2 bulletPos, Vector2 bulletDirection, float bulletSpeed)
        {
            startPosition = CurrentPosition = PreviousPosition = bulletPos;
            id = bulletId;
            direction = bulletDirection;
            speed = bulletSpeed;
        }


        public bool MoveBullet()
        {
            if (Mathf.Abs(CurrentPosition.x) > Helps.MaxWorldPosition
                || Mathf.Abs(CurrentPosition.y) > Helps.MaxWorldPosition)
            {
                return false;
            }

            PreviousPosition = CurrentPosition;
            CurrentPosition += direction * speed * Time.deltaTime;
            return true;
        }
        #endregion
    }
}