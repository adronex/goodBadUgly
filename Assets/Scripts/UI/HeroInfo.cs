using UnityEngine;

namespace UI
{
    public class HeroInfo : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private int hp;
        [SerializeField] private int ammo;
        [SerializeField] private Transform handAxis;
        [SerializeField] private Transform gunpoint;
        [SerializeField] private Transform areas;

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

        public Transform HandAxis
        {
            get { return handAxis; }
        }

        public Transform Gunpoint
        {
            get { return gunpoint; }
        }

        public Transform Areas
        {
            get { return areas; }
        }
    }
}