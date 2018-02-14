using UnityEngine;

public class Hero
{
    public static event HealthChangedEventHandler HealthChangedEvent;
    public static event HeroDeadEventHandler HeroDeadEvent;
    public static event AmmoChangedEventHandler AmmoChangedEvent;

    private int id;
    private int fullHealth;
    private int currentHealth;
    private int maxAmmo;
    private int currentAmmo;
    private Vector2 pos;

    public int CurrentAmmo
    {
        get { return currentAmmo; }
        set
        {
            if (value < 0)
            {
                return;
            }

            currentAmmo = value;

            if (AmmoChangedEvent != null)
            {
                AmmoChangedEvent(id, currentAmmo);
            }
        }
    }


    public void Damage(int damage)
    {
        currentHealth -= damage;
        currentAmmo--;

        if (HealthChangedEvent != null)
        {
            HealthChangedEvent(id, currentHealth);
        }

        if (currentHealth <= 0)
        {
            if (HeroDeadEvent != null)
            {
                HeroDeadEvent(id);
            }
        }
    }


    public Hero(int id, int fullHealth, int maxAmmo, Vector2 pos)
    {
        this.id = id;
        this.fullHealth = fullHealth;
        this.maxAmmo = maxAmmo;
        this.pos = pos;
        currentHealth = fullHealth;
        currentAmmo = maxAmmo;
    }


    public delegate void HealthChangedEventHandler(int id, int currentHealth);
    public delegate void HeroDeadEventHandler(int id);
    public delegate void AmmoChangedEventHandler(int id, int currentAmmo);
}
