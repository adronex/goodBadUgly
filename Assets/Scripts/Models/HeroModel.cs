using UnityEngine;

public class HeroModel
{
    public Hero Hero;
    public int FullHealth;
    public int CurrentHealth;
    public int MaxAmmo;
    public int CurrentAmmo;
    public Transform Hand;
    public BodyPartModel[] BodyParts;

    public bool CanShoot { get { return CurrentAmmo > 0; } }

    public HeroModel(Hero hero, int fullHealth, int maxAmmo)
    {
        Hero = hero;
        FullHealth = fullHealth;
        CurrentHealth = fullHealth;
        MaxAmmo = maxAmmo;
        CurrentAmmo = maxAmmo;
        Hand = App.ownHand.transform.Find("HandAxis");
    }
}
