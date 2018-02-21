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

    private BodyPart[] bodyParts;
    #endregion
    #region Properties
//    public HeroType Type
//    {
//        get { return type; }
//    }

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

//    public int MaxAmmo
//    {
//        get { return maxAmmo; }
//    }

    public int CurrentAmmo
    {
        get { return currentAmmo; }

    }

    public BodyPart[] BodyParts
    {
        get { return bodyParts; }
    }

    #endregion
    #region Public methods

    public Hero(HeroType type, int health, int ammo)
    {
        this.type = type;
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