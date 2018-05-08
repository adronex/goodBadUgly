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
            var rotation = gunpoint.rotation * Quaternion.Euler(0, 0, -90);

            instanse.bulletPool.Create(position, rotation, hero.BulletSpeed);

            AudioManager.Shoot(hero);
        }


        public static void CreateBlood(Transform collision, Transform bullet)
        {
            AudioManager.Hurt(collision);

            instanse.bloodHolePool.Create(bullet);

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

            bloodPool.Create(bullet.position, bullet.rotation);
        }
        #endregion

        #region Private methods
        private void CreateBloodHole(Collision2D collision, Transform bullet)
        {
            var offset = Random.Range(0, 0.7f);
            var position = bullet.position + bullet.right * offset;

            var ass = Physics2D.CircleCastAll(position, 0.1f, Vector2.zero);

            print(ass.Length);

            for (int i = 0; i < ass.Length; i++)
            {
                Instantiate(bloodHolePrefab, position, Quaternion.identity, ass[i].transform);
            }
        }
        #endregion
    }
}