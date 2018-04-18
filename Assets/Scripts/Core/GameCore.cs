using Controller;
using UnityEngine;

public class GameCore
{
    #region Fields
    public static event GameStartingEventHandler GameStartingEvent;
    public static event GameWaitingEventHandler GameWaitingEvent;
    public static event StartingCountdownEventHandler StartingCountdownEvent;
    public static event GameEndingEventHandler GameEndingEvent;

    private readonly Hero ownHero;
    private readonly Hero enemyHero;
    #endregion
    #region Properties
    public GameState CurrentGameState { get; private set; }

    public Hero OwnHero
    {
        get { return ownHero; }
    }

    public Hero EnemyHero
    {
        get { return enemyHero; }
    }

    public Transform GetGunpoint(Hero hero)
    {
        return hero.Gunpoint;
    }

    #endregion
    #region Public methods
    public GameCore(HeroInfo ownHeroInfo, HeroInfo enemyHeroInfo)
    {
        CurrentGameState = GameState.Waiting;

        ownHero = new OwnHero(ownHeroInfo);
        enemyHero = new EnemyHero(enemyHeroInfo);
    }

    public void Shoot(Hero hero)
    {
        hero.Shoot();
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

    public void StartWaiting()
    {
        CurrentGameState = GameState.Waiting;
        if (GameWaitingEvent != null)
        {
            GameWaitingEvent();
        }
    }

    public void StartCountdown()
    {
        CurrentGameState = GameState.Countdown;
        if (StartingCountdownEvent != null)
        {
            StartingCountdownEvent();
        }
    }

    public void StartGame()
    {
        CurrentGameState = GameState.Battle;
        if (GameStartingEvent != null)
        {
            GameStartingEvent();
        }
    }

    public bool CheckCollision(Hero hero, Vector2 previous, Vector2 current)
    {
        var bodyParts = hero.BodyParts;

        var bodyPartId = CollisionController.GetCollidedBodyPartId(previous, current, bodyParts);
        if (bodyPartId == -1)
        {
            return false;
        }

        var bodyPart = bodyParts[bodyPartId];

        hero.PlayAnimation(bodyPartId);
        Damage(hero, bodyPart.Damage);
        CheckEndGame(hero);
        return true;
    }


    public void RotateHand(Hero hero, Vector2 aim)
    {
        hero.RotateHand(aim);
    }
    #endregion
    #region Private methods
    private void CheckEndGame(Hero hero)
    {
        if (CurrentGameState == GameState.Battle && hero.IsDead)
        {
            CurrentGameState = GameState.End;

            if (GameEndingEvent != null)
            {
                GameEndingEvent();
            }
        }
    }


    private void Damage(Hero hero, int damageCount)
    {
        hero.ReduceHp(damageCount);
    }
    #endregion
    #region Event handlers
    public delegate void GameStartingEventHandler();
    public delegate void GameWaitingEventHandler();
    public delegate void StartingCountdownEventHandler();
    public delegate void GameEndingEventHandler();
    #endregion
}

