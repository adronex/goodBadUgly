using UnityEngine;

namespace UI
{
    public class HeroInfo : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private int hp;
        [SerializeField] private int ammo;
        [SerializeField] private Transform gunpoint;
        [SerializeField] private Transform areas;
        [SerializeField] private Transform body;
        [SerializeField] private Transform hero;
        [SerializeField] private Transform leftLeg;
        [SerializeField] private Transform rightLeg;
        [SerializeField] private Transform falseStart;


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

        public Animator Animator
        {
            get { return GetComponent<Animator>(); }
        }

        public Transform Gunpoint
        {
            get { return gunpoint; }
        }

        public Transform Areas
        {
            get { return areas; }
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
    }
}