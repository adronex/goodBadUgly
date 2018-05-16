using Core;
using UnityEngine;

namespace Graphics
{
    public class ClientGraphic : MonoBehaviour
    {
        #region Fields
        [SerializeField] GameObject bulletPrefab;
        [SerializeField] GameObject headBloodPrefab;
        [SerializeField] GameObject bodyBloodPrefab;
        [SerializeField] GameObject legBloodPrefab;
        [SerializeField] GameObject bloodHolePrefab;

        BulletPool bulletPool;
        BloodPool headBloodPool;
        BloodPool bodyBloodPool;
        BloodPool legBloodPool;
        BloodHolePool bloodHolePool;

        private static ClientGraphic instanse;
        #endregion

        #region Unity lifecycle
        private void Awake()
        {
            bulletPool = new BulletPool(bulletPrefab);
            headBloodPool = new BloodPool(headBloodPrefab);
            bodyBloodPool = new BloodPool(bodyBloodPrefab);
            legBloodPool = new BloodPool(legBloodPrefab);
            bloodHolePool = new BloodHolePool(bloodHolePrefab);

            instanse = this;
        }
        #endregion

        #region Public fields
        public static void CreateBullet(Vector2 position, Quaternion rotation, float speed)
        {
            instanse.bulletPool.Create(position, rotation, speed);
        }

        public static void CreateBullet(Hero hero)
        {
            var gunpoint = hero.Gunpoint;

            var position = gunpoint.position - hero.Offset;
            var rotation = gunpoint.rotation * Quaternion.Euler(0f, 0f, -90f);

            instanse.bulletPool.Create(position, rotation, hero.BulletSpeed);

            AudioManager.Shoot(hero);
        }


        public static void CreateBlood(Transform collision, Transform bullet, Vector3 offset)
        {
            AudioManager.Hurt(collision);

            var bulletSpeed = bullet.GetComponent<Bullet>().Speed;
            var randomOffset = offset * Random.Range(1.3f, 1.7f);

            var bloodHolePos = bullet.position + randomOffset * bulletSpeed * Time.deltaTime;
            instanse.bloodHolePool.Create(bloodHolePos);

            var bodyPartName = collision.GetComponent<BodyPartName>().Name;

            BloodPool bloodPool;
            switch (bodyPartName)
            {
                case BodyPartName.BodyPart.Head:
                    bloodPool = instanse.headBloodPool;
                    break;
                case BodyPartName.BodyPart.Body:
                    bloodPool = instanse.bodyBloodPool;
                    break;
                case BodyPartName.BodyPart.Leg:
                    bloodPool = instanse.legBloodPool;
                    break;
                default:
                    throw new UnityException();
            }

            bloodPool.Create(bullet.position + offset, bullet.rotation);
        }
        #endregion
    }
}