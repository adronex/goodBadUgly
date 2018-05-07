using UnityEngine;
using Object = UnityEngine.Object;

namespace Graphics
{
    public class BulletPool
    {
        #region Fields
        private const int MAX_BULLETS = 4;

        private Bullet[] bullets;

        private Transform bulletStorage;
        #endregion

        #region Public Fields
        internal BulletPool(GameObject bulletPrefab)
        {
            bulletStorage = new GameObject().transform;
            bulletStorage.name = "BulletStorage";

            bullets = new Bullet[MAX_BULLETS];
            for (int i = 0; i < MAX_BULLETS; i++)
            {
                var bulletObject = Object.Instantiate(bulletPrefab, bulletStorage);
                var bullet = bulletObject.GetComponent<Bullet>();
                bulletObject.SetActive(false);

                bullets[i] = bullet;
            }
        }


        internal void Create(Vector2 position, Quaternion rotation, float speed)
        {
            Bullet bullet;
            for (int index = 0; index < MAX_BULLETS; index++)
            {
                bullet = bullets[index];
                if (bullet.IsBusy)
                {
                    bullet.Create(position, rotation, speed);
                    return;
                }
            }

            //if all bullets are busy
            var farthestIndex = 0;
            var farthest = 0f;
            float distance;
            for (int index = 0; index < MAX_BULLETS; index++)
            {
                distance = bullets[index].DistanceFromWorldCenter;
                if (farthest < distance)
                {
                    farthest = distance;
                    farthestIndex = index;
                }
            }

            bullets[farthestIndex].Create(position, rotation, speed);
        }
        #endregion
    }
}