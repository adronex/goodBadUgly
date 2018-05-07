using UnityEngine;

namespace Core
{
    public class BulletInfo
    {
        private int id;
        private Vector2 direction;
        private float speed;
        private Vector2 startPosition;

        public Vector2 CurrentPosition { get; private set; }
        public Vector2 PreviousPosition { get; private set; }

        public float ImpactAngle
        {
            get { return Vector2.Angle(startPosition, CurrentPosition); }
        }


        public BulletInfo(int bulletId, Vector2 bulletPos, Vector2 bulletDirection, float bulletSpeed)
        {
            id = bulletId;
            startPosition = CurrentPosition = PreviousPosition = bulletPos;
            direction = bulletDirection;
            speed = bulletSpeed;
        }

        public bool MoveBullet()
        {
            //if (Mathf.Abs(CurrentPosition.x) > 20 && Mathf.Abs(CurrentPosition.y) > 20)
            //{
            //    Bullet.DestroyBullet(Transform);
            //    return false;
            //}

            PreviousPosition = CurrentPosition;
            CurrentPosition += direction * speed * Time.deltaTime;
            return true;
        }
    }
}