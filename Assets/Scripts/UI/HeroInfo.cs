using UnityEngine;

namespace UI
{
    public class HeroInfo : MonoBehaviour
    {
        #region Fields
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private int hp;
        [SerializeField] private int ammo;
        [SerializeField] private Transform gunpoint;
        [SerializeField] private Transform hitbox;
        [SerializeField] private Transform body;
        [SerializeField] private Transform hero;
        [SerializeField] private Transform leftLeg;
        [SerializeField] private Transform rightLeg;
        [SerializeField] private Transform falseStart;
        [SerializeField] private Sprite bulletSprite;
        [SerializeField] private Transform hat;
        #endregion
        #region Properties
        public GameObject BulletPrefab
        {
            get { return bulletPrefab; }
        }


        public float BulletSpeed
        {
            get { return bulletSpeed; }
        }


        public int Hp
        {
            get { return hp; }
        }


        public int Ammo
        {
            get { return ammo; }
        }


        public Transform Gunpoint
        {
            get { return gunpoint; }
        }


        public Transform HitBox
        {
            get { return hitbox; }
        }


        public Transform Body
        {
            get { return body; }
        }


        public Transform Hero
        {
            get { return hero; }
        }


        public Transform LeftLeg
        {
            get { return leftLeg; }
        }


        public Transform RightLeg
        {
            get { return rightLeg; }
        }


        public Transform FalseStart
        {
            get { return falseStart; }
        }


        public Sprite BulletSprite
        {
            get { return bulletSprite; }
        }


        public Transform Hat
        {
            get { return hat; }
        }


        public Animator Animator
        {
            get { return GetComponent<Animator>(); }
        }
        #endregion
    }
}