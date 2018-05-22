using Core.Heroes;
using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
#region Fields
        private static AudioManager instanse;

        [SerializeField] private AudioClip shoot;
        [SerializeField] private AudioClip hurt;
        [SerializeField] [Range(0, 1)] private float shootVolume;
        [SerializeField] [Range(0, 1)] private float hurtVolume;

        private AudioSource ownSource;
        private AudioSource enemySource;
        #endregion
#region Unity lifecycle
        private void Awake()
        {
            instanse = this;
        }


        private void Start()
        {
            ownSource = GameObject.Find(Helps.OwnHeroName).GetComponent<AudioSource>();
            enemySource = GameObject.Find(Helps.EnemyHeroName).GetComponent<AudioSource>();
        }
        #endregion
#region Public methods
        public static void Shoot(Hero hero)
        {
            if (hero as OwnHero != null)
            {
                instanse.OwnShoot();
            }
            else if (hero as EnemyHero != null)
            {
                instanse.EnemyShoot();
            }
        }


        public void HeroHurt(Transform hero)
        {
            var source = hero.GetComponent<AudioSource>();

            if (enemySource.volume != hurtVolume)
            {
                enemySource.volume = hurtVolume;
            }

            source.PlayOneShot(hurt);
        }


        public static void Hurt(Transform collision)
        {
            var hero = GetRootObject(collision);
            instanse.HeroHurt(hero);
        }
        #endregion
        #region Private methods
        private void OwnShoot()
        {
            if (ownSource.volume != shootVolume)
            {
                ownSource.volume = shootVolume;
            }

            ownSource.PlayOneShot(shoot);
        }


        private void EnemyShoot()
        {
            if (enemySource.volume != shootVolume)
            {
                enemySource.volume = shootVolume;
            }

            enemySource.PlayOneShot(shoot);
        }

        
        private static Transform GetRootObject(Transform collision)
        {
            var parent = collision.parent;
            if (parent == null)
            {
                return collision;
            }

            return GetRootObject(parent);
        }
        #endregion
    }
}
