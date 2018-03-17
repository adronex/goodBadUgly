using Controller;
public class OwnHero : Hero
{
    public OwnHero(HeroesManager heroesManager, int startHp, int startBullets) : base(startHp, startBullets)
    {
        var axis = heroesManager.OwnHandAxis;
        hand = new HandController(axis);

        ammoText = heroesManager.OwnAmmoText;
        hpImage = heroesManager.OwnHpImage;
        handAxis = heroesManager.OwnHandAxis;
    }
}
