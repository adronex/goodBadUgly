namespace GameCore.Model
{
    public class Hero
    {
        private int maxHealth;
        private int currentHealth;
        private int maxAmmo;
        private int currentAmmo;
        
        //private collider headCollider
        //private collider bodyCollider
        //private collider legsCollider

        public int MaxHealth
        {
            get { return maxHealth; }
        }

        public int CurrentHealth
        {
            get { return currentHealth; }
        }

        public int MaxAmmo
        {
            get { return maxAmmo; }
        }

        public int CurrentAmmo
        {
            get { return currentAmmo; }
        }
    }
}