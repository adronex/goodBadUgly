using UnityEngine;
using Object = UnityEngine.Object;

namespace Graphics
{
    public class BulletPool
    {
        #region Fields
        private Bullet[] bullets;

        private Transform bulletStorage;
        #endregion
        #region Public Fields
        internal BulletPool(GameObject bulletPrefab)
        {
            bulletStorage = new GameObject().transform;
            bulletStorage.name = Helps.BulletStorageName;

            bullets = new Bullet[Helps.BulletLimit];
            for (int index = 0; index < Helps.BulletLimit; index++)
            {
                var bulletObject = Object.Instantiate(bulletPrefab, bulletStorage);
                var bullet = bulletObject.GetComponent<Bullet>();
                bulletObject.SetActive(false);

                bullets[index] = bullet;
            }
        }


        internal void Create(Sprite bulletSprite, Vector2 position, Quaternion rotation, float speed)
        {
            Bullet bullet;
            for (int index = 0; index < Helps.BulletLimit; index++)
            {
                bullet = bullets[index];
                if (bullet.IsBusy)
                {
                    bullet.Create(bulletSprite, position, rotation, speed);
                    return;
                }
            }

            var farthestBullet = GetFarthestBullet(position, rotation, speed);
            farthestBullet.Create(bulletSprite, position, rotation, speed);
        }
        #endregion
        #region Private methods
        private Bullet GetFarthestBullet(Vector2 position, Quaternion rotation, float speed)
        {
            var farthestIndex = -1;
            var farthest = float.MaxValue;
            float distance;
            for (int index = 0; index < Helps.BulletLimit; index++)
            {
                distance = bullets[index].DistanceFromWorldCenter;
                if (farthest < distance)
                {
                    farthest = distance;
                    farthestIndex = index;
                }
            }

            return bullets[farthestIndex];
        }
        #endregion
    }
}