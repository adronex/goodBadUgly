using Controller;
public class EnemyHero : Hero
{
    public EnemyHero(HeroesManager heroesManager, int startHp, int startBullets) : base(startHp, startBullets)
    {
        var axis = heroesManager.EnemyHandAxis;
        hand = new HandController(axis);

        ammoText = heroesManager.EnemyAmmoText;
        hpImage = heroesManager.EnemyHpImage;
        handAxis = heroesManager.EnemyHandAxis;

        bodyParts = BodyPart.FindEnemyParts();////////////////////////////////////////////////////////
    }
}
