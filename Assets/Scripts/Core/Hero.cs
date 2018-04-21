using Controller;
using UI;
using UnityEngine;

namespace Core
{

    public class BulletInfo
    {
        public int bulletId;
        public Vector2 currentBulletPos;
        public Vector2 previousBulletPos;
        public Vector2 bulletDirection;
        public float bulletSpeed;
        public Transform transform;

        public bool MoveBullet()
        {
            if (Mathf.Abs(currentBulletPos.x) > 20 && Mathf.Abs(currentBulletPos.y) > 20)
            {
                Bullet.DestroyBullet(transform);
                return false;
            }

            previousBulletPos = currentBulletPos;
            currentBulletPos += bulletDirection * bulletSpeed * Time.deltaTime;
            return true;
        }
    }


    public abstract class Hero
    {
        #region Fields 
        private static readonly Vector3 GUNPOINT_OFFSET = new Vector3(-0.25f, 0.8f);

        public static event HpChangedEventHandler HpChangedEvent;
        public static event AmmoChangedEventHandler AmmoChangedEvent;

        public delegate void HpChangedEventHandler(Hero hero, int currentHp, int maxHp);
        public delegate void AmmoChangedEventHandler(Hero hero, int currentAmmo, int maxAmmo);

        private const float ONE_PERCENT = 0.01f;

        protected int maxHp;
        protected int currentHp;
        protected int maxAmmo;
        protected int currentAmmo;

        protected Animator animator;
        protected HandController hand;
        protected BodyPart[] bodyParts;
        protected Transform handAxis;

        protected GameObject bulletPrefab;
        protected float bulletSpeed;

        protected BulletInfo[] bullets = new BulletInfo[6];

        public BulletInfo[] GetBullets
        {
            get
            {
                return bullets;
            }
        }

        internal void MoveBullets()
        {
            var length = bullets.Length;
            for (int i = 0; i < length; i++)
            {
                var bullet = bullets[i];

                if (bullet != null)
                {
                    if (!bullet.MoveBullet())
                    {
                        bullets[i] = null;
                    }
                }
            }
        }

        protected int currentBulletID;

        #endregion
        #region Properties
        public Transform Gunpoint
        {
            get { return hand.GunPoint; }
        }

        public Vector3 Offset
        {
            get { return Gunpoint.TransformDirection(GUNPOINT_OFFSET); }
        }

        public BodyPart[] BodyParts
        {
            get { return bodyParts; }
        }

        public bool IsDead
        {
            get { return currentHp <= 0; }
        }
        #endregion
        #region Public Methods


        public void Damage(int amount)
        {
            currentHp -= amount;
            if (HpChangedEvent != null)
            {
                HpChangedEvent(this, currentHp, maxHp);
            }
        }


        public void RotateHand(Vector2 aim)
        {
            hand.LookTo(aim);
        }


        public void PlayAnimation(int bodyPartId)
        {
            var angle = Random.Range(0, 90);
            animator.SetInteger("BodyPart", bodyPartId);
            animator.SetFloat("Angle", angle);
            animator.SetTrigger("Do");
        }

        public bool CanShoot
        {
            get
            {
                return currentAmmo > 0;
            }
        }

        public bool Shoot()
        {
            if (!CanShoot)
            {
                return false;
            }

            Vector2 bulletDir = Vector2.zero;

            Quaternion addRotation;
            
            var bul = Bullet.CreateBullet(bulletPrefab, bulletSpeed, Gunpoint, Offset, out bulletDir);

            AddBullet(bulletDir, bul);

            ReduceAmmo();

            return true;
        }

        private void AddBullet(Vector2 dir, Transform bul)
        {
            var newBullet = new BulletInfo
            {
                bulletId = currentBulletID++,
                currentBulletPos = Gunpoint.position - Offset,
                previousBulletPos = Gunpoint.position - Offset,
                bulletDirection = dir,
                bulletSpeed = bulletSpeed,
                transform = bul,
            };

            var length = bullets.Length;
            for (int i = 0; i < length; i++)
            {
                if (bullets[i] != null)
                {
                    continue;
                }

                bullets[i] = newBullet;
                return;
            }
        }

        #endregion
        #region Private Methods
        protected Hero(HeroInfo heroInfo)
        {
            maxHp = heroInfo.Hp;
            currentHp = maxHp;

            if (HpChangedEvent != null)
            {
                HpChangedEvent(this, maxHp, maxHp);
            }

            maxAmmo = heroInfo.Ammo;
            currentAmmo = maxAmmo;

            if (AmmoChangedEvent != null)
            {
                AmmoChangedEvent(this, maxAmmo, maxAmmo);
            }

            bulletPrefab = heroInfo.BulletPrefab;
            bulletSpeed = heroInfo.BulletSpeed;

            animator = heroInfo.Animator;

            hand = new HandController(heroInfo);

            bodyParts = BodyPart.FindEnemyParts(heroInfo.Areas);
        }


        private void ReduceAmmo()
        {
            currentAmmo--;

            if (AmmoChangedEvent != null)
            {
                AmmoChangedEvent(this, currentAmmo, maxAmmo);
            }
        }
        #endregion
    }
}