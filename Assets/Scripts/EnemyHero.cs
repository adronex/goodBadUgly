using Controller;
using UnityEngine;

public class EnemyHero : Hero
{
    public EnemyHero(HeroesManager heroesManager, int startHp, int startBullets, Animator animator) : base(startHp, startBullets, animator)
    {
        var axis = heroesManager.EnemyHandAxis;
        hand = new HandController(axis);

        ammoText = heroesManager.EnemyAmmoText;
        hpImage = heroesManager.EnemyHpImage;
        handAxis = heroesManager.EnemyHandAxis;

        //todo: 
        bodyParts = BodyPart.FindEnemyParts();
    }
}
