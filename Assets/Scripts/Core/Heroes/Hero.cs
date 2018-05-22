using Graphics;
using UI;
using UnityEngine;

namespace Core.Heroes
{
    public abstract class Hero
    {
        #region Fields 
        public static event HpChangedEventHandler HpChangedEvent;
        public static event AmmoChangedEventHandler AmmoChangedEvent;

        public delegate void HpChangedEventHandler(Hero hero, int currentHp, int maxHp);
        public delegate void AmmoChangedEventHandler(Hero hero, int currentAmmo, int maxAmmo);

        protected int maxHp;
        protected int currentHp;
        protected int maxAmmo;
        protected int currentAmmo;

        protected Animator animator;
        protected HandController hand;
        protected HitBox[] bodyParts;
        protected Transform handAxis;

        protected Sprite bulletSprite;
        protected float bulletSpeed;

        protected BulletInfo[] bullets;
        protected int currentBulletID;
        #endregion
        #region Properties
        public BulletInfo[] GetBullets
        {
            get { return bullets; }
        }


        public Transform Gunpoint
        {
            get { return hand.Gunpoint; }
        }


        public HitBox[] BodyParts
        {
            get { return bodyParts; }
        }


        public bool IsDead
        {
            get { return currentHp <= Helps.MinHp; }
        }


        public bool CanShoot
        {
            get { return currentAmmo > Helps.MinAmmo; }
        }


        public float BulletSpeed
        {
            get { return bulletSpeed; }
        }


        public Sprite BulletSprite
        {
            get { return bulletSprite; }
        }


        private AnimatorStateInfo CurrentAnimatorStateInfo
        {
            get { return animator.GetCurrentAnimatorStateInfo(0); }
        }
        #endregion
        #region Public Methods
        protected Hero(HeroInfo heroInfo)
        {
            maxHp = heroInfo.Hp;
            currentHp = maxHp;

            if (HpChangedEvent != null)
            {
                HpChangedEvent(this, currentHp, maxHp);
            }

            maxAmmo = heroInfo.Ammo;
            currentAmmo = maxAmmo;

            if (AmmoChangedEvent != null)
            {
                AmmoChangedEvent(this, currentAmmo, maxAmmo);
            }

            bullets = new BulletInfo[maxAmmo];

            bulletSprite = heroInfo.BulletSprite;
            bulletSpeed = heroInfo.BulletSpeed;

            animator = heroInfo.Animator;

            hand = new HandController(heroInfo);

            bodyParts = HitBox.GetHitBox(heroInfo.HitBox);
        }


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
            if (aim == Vector2.zero)
            {
                return;
            }

            if (!CanRotate())
            {
                return;
            }

            hand.LookTo(aim);
        }


        public void PlayAnimation(BulletInfo bullet, int bodyPartId)
        {
            animator.SetInteger(Helps.BodyPartAnim, bodyPartId);
            animator.SetTrigger(Helps.HitAnim);
        }


        public bool Shoot()
        {
            if (!CanShoot)
            {
                return false;
            }

            if (CurrentAnimatorStateInfo.IsName(Helps.FalseStartAnim))
            {
                return false;
            }

            ClientGraphic.CreateBullet(this);

            AddBullet();

            ReduceAmmo();

            return true;
        }


        internal void MoveBullets()
        {
            var length = bullets.Length;
            for (int index = 0; index < length; index++)
            {
                var bullet = bullets[index];

                if (bullet != null)
                {
                    if (!bullet.MoveBullet())
                    {
                        bullets[index] = null;
                    }
                }
            }
        }


        internal void PlayFalseStart()
        {
            if (CurrentAnimatorStateInfo.IsName(Helps.FalseStartAnim))
            {
                return;
            }

            animator.SetTrigger(Helps.FalseStartAnim);
        }
        #endregion
        #region Private Methods
        private void AddBullet()
        {
            var dir = Gunpoint.rotation * Helps.BulletStartRotation * Vector2.right;

            var newBullet = new BulletInfo(currentBulletID, Gunpoint.position, dir, bulletSpeed);
            currentBulletID++;

            var length = bullets.Length;
            for (int index = 0; index < length; index++)
            {
                if (bullets[index] != null)
                {
                    continue;
                }

                bullets[index] = newBullet;
                return;
            }
        }


        private void ReduceAmmo()
        {
            currentAmmo--;

            if (AmmoChangedEvent != null)
            {
                AmmoChangedEvent(this, currentAmmo, maxAmmo);
            }
        }


        private bool CanRotate()
        {
            var currentAnimState = CurrentAnimatorStateInfo;
            if (!currentAnimState.IsName(Helps.IdleAnim))
            {
                if (currentAnimState.length > currentAnimState.normalizedTime)
                {
                    return false;
                }
            }

            return true;
        }
        #endregion
    }
}