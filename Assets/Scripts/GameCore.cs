using Controller;
using UnityEngine;

public class GameCore
{
    #region Fields
    public static event GameDoStartedEventHandler GameDoStartedEvent; //todo: change events/delegates names
    public static event GameDoWaitedEventHandler GameDoWaitedEvent;
    public static event GameDoCountdownEventHandler GameDoCountdownEvent;
    public static event GameOverEventHandler GameOverEvent;

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

        ownHero = new OwnHero(heroesManager, 100, 6, GameObject.Find("OwnCowboy").GetComponent<Animator>());
        enemyHero = new EnemyHero(heroesManager, 100, 6, GameObject.Find("EnemyCowboy").GetComponent<Animator>());
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

    public bool CheckCollision(Hero hero, Transform bullet)
    {
        var gunpoint = hero.Gunpoint;
        var gunpointPos = gunpoint.position;

        var bodyParts = hero.BodyParts;

        int partId;
        int damageCount = CollisionController.CheckCollision(bullet.position, gunpointPos, bodyParts, out partId);
        if (damageCount <= 0)
        {
            return false;
        }

        hero.PlayAnimation(partId);
        Damage(hero, damageCount);
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

            if (GameOverEvent != null)
            {
                GameOverEvent();
            }
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
    public delegate void GameOverEventHandler();
    #endregion
}

