using Controller;

namespace Model
{
    public class Hero
    {
        #region Fields
        private HeroType type;
        private int maxHealth;
        private int currentHealth;
        private int maxAmmo;
        private int currentAmmo;

        private HandController hand;
        private BodyPart[] bodyParts;
        #endregion
        #region Properties
        public int CurrentHealth
        {
            get { return currentHealth; }
            set { currentHealth = value; }
        }

        public int CurrentAmmo
        {
            get { return currentAmmo; }
            set { currentAmmo = value; }
        }

        public BodyPart[] BodyParts
        {
            get { return bodyParts; }
        }

        public HandController Hand
        {
            get { return hand; }
        }

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
        #endregion
    }
}