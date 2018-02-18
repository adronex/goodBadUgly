using UnityEngine;

public class Model : MonoBehaviour
{
    public HeroModel OwnHero;
    public HeroModel EnemyHero;
    public AreaModel[] Areas;

    private void Start()
    {
        OwnHero = new HeroModel(Hero.Own, fullHealth: 100, maxAmmo: 6);
        EnemyHero = new HeroModel(Hero.Enemy, fullHealth: 100, maxAmmo: 6);

        Areas = new AreaModel[3];
        Areas[0] = new AreaModel(Area.AimArea, GameObject.Find("AimArena").GetComponent<RectTransform>());
        Areas[1] = new AreaModel(Area.HeroArea, GameObject.Find("HeroArena").GetComponent<RectTransform>());
        Areas[2] = new AreaModel(Area.ShootArea, GameObject.Find("ShootArena").GetComponent<RectTransform>());
    }
}
