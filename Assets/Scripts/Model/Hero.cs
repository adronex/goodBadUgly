using Controller;

namespace Model
{
    public class Hero
    {
        #region Fields
        public static event HealthChangedEventHandler HealthChangedEvent;
        public static event AmmoChangedEventHandler AmmoChangedEvent;

        private HeroType type;
        private int maxHealth;
        private int currentHealth;
        private int maxAmmo;
        private int currentAmmo;

        private HandController hand;
        private BodyPart[] bodyParts;
        #endregion
        #region Properties
        public int MaxHealth
        {
            get { return maxHealth; }
        }

        public int CurrentHealth
        {
            get { return currentHealth; }
            set
            {
                currentHealth = value;
                if (HealthChangedEvent != null)
                {
                    HealthChangedEvent(type, currentHealth);
                }
            }
        }

        public int CurrentAmmo
        {
            get { return currentAmmo; }

        }

        public BodyPart[] BodyParts
        {
            get { return bodyParts; }
        }

        public HandController Hand { get { return hand; } }

        #endregion
        #region Public methods

        public Hero(HeroType type, HandController hand, int health, int ammo)
        {
            this.type = type;
            this.hand = hand;
            maxHealth = health;
            currentHealth = health;
            maxAmmo = ammo;
            currentAmmo = ammo;

            if (type == HeroType.Enemy)
            {
                bodyParts = BodyPart.FindEnemyParts();
            }
        }

        public void OnShoot()
        {
            currentAmmo--;
            if (AmmoChangedEvent != null)
            {
                AmmoChangedEvent(type, CurrentAmmo);
            }
        }
        #endregion

        #region Event handlers
        public delegate void HealthChangedEventHandler(HeroType type, int currentHealth);
        public delegate void AmmoChangedEventHandler(HeroType type, int currentAmmo);
        #endregion
    }
}