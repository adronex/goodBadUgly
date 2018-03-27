using Controller;
using UnityEngine;

public class OwnHero : Hero
{
    public OwnHero(HeroesManager heroesManager, int startHp, int startBullets, Animator animator) : base(startHp, startBullets,animator)
    {
        var axis = heroesManager.OwnHandAxis;
        hand = new HandController(axis);

        ammoText = heroesManager.OwnAmmoText;
        hpImage = heroesManager.OwnHpImage;
        handAxis = heroesManager.OwnHandAxis;
    }
}
