using Controller;
using UnityEngine;

public class GameCore
{
    #region Fields
    public static event GameDoStartedEventHandler GameDoStartedEvent; //todo: change events/delegates names
    public static event GameDoWaitedEventHandler GameDoWaitedEvent;
    public static event GameDoCountdownEventHandler GameDoCountdownEvent;

    private readonly Hero ownHero;
    private readonly Hero enemyHero;

    #endregion
    #region Properties
    public GameState CurrentGameState { get; private set; }

    public Hero OwnHero
    {
        get
        {
            return ownHero;
        }
    }

    public Hero EnemyHero
    {
        get
        {
            return enemyHero;
        }
    }

    public Transform GetGunpoint(Hero hero) //todo: add the enemy gunpoint
    {
        return hero.Gunpoint;
    }

    #endregion
    #region Public methods
    public GameCore(HeroesManager heroesManager)
    {
        CurrentGameState = GameState.Waiting;

        ownHero = new OwnHero(heroesManager, 100, 6);
        enemyHero = new EnemyHero(heroesManager, 100, 6);
    }

    public bool CanShoot()
    {
        if (ownHero.CanShoot)
        {
            return false;
        }

        ownHero.ReduceAmmo();
        return true;
    }

    public void StartWaiting() //the logic of punishment
    {
        CurrentGameState = GameState.Waiting;
        if (GameDoWaitedEvent != null)
        {
            GameDoWaitedEvent();
        }
    }

    public void StartCountdown()
    {
        CurrentGameState = GameState.Countdown;
        if (GameDoCountdownEvent != null)
        {
            GameDoCountdownEvent();
        }
    }

    public void StartGame()
    {
        CurrentGameState = GameState.Battle;
        if (GameDoStartedEvent != null)
        {
            GameDoStartedEvent();
        }
    }

    public bool CheckCollision(Hero hero, Vector2 bulletPos)
    {
        var gunpoint = hero.Gunpoint;
        var gunpointPos = gunpoint.position;

        var bodyParts = hero.BodyParts;
        int damageCount = CollisionController.CheckCollision(bulletPos, gunpointPos, bodyParts);
        if (damageCount > 0)
        {
            Damage(hero, damageCount);
            CheckEndGame();
            return true;
        }

        return false;
    }


    public void RotateHand(Hero hero, Vector2 aim)
    {
        hero.RotateHand(aim);
    }
    #endregion
    #region Private methods
    private void CheckEndGame()
    {
        var anyIsDead = ownHero.IsDead || ownHero.IsDead;
        if (CurrentGameState == GameState.Battle && anyIsDead)
        {
            CurrentGameState = GameState.End;
        }
    }


    private void Damage(Hero hero, int damageCount)
    {
        hero.ReduceHp(damageCount);
    }
    #endregion
    #region Event handlers
    public delegate void GameDoStartedEventHandler();
    public delegate void GameDoWaitedEventHandler();
    public delegate void GameDoCountdownEventHandler();
    #endregion
}