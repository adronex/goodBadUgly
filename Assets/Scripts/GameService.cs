using UnityEngine;

public class GameService
{
    private Hero ownHero;
    private Hero enemyHero;

    public bool TryShoot { get { return ownHero.CurrentAmmo-- > 0; } }

    public void DamageEnemy(int damage)
    {
        enemyHero.Damage(damage);
    }


    public GameService(Transform gunpoint) : base()
    {
        var enemy = GameObject.Find("EnemyHero").transform;
        var own = GameObject.Find("OwnHero").transform;

        ownHero = new Hero(id: 0, fullHealth: 100, maxAmmo: 6, pos: own.position);
        enemyHero = new Hero(id: 1, fullHealth: 100, maxAmmo: 6, pos: enemy.position);
    }


    public void Shoot(float angle)
    {
    }


    public void CheckEndGame()
    {
    }
}
