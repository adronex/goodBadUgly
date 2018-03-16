using System;
using Controller;
using Model;
using UnityEngine;

public class GameCore
{
    #region Fields
    public static event GameDoStartedEventHandler GameDoStartedEvent; //todo: change events/delegates names
    public static event GameDoWaitedEventHandler GameDoWaitedEvent;
    public static event GameDoCountdownEventHandler GameDoCountdownEvent;

    public static event HealthChangedEventHandler HealthChangedEvent;
    public static event AmmoChangedEventHandler AmmoChangedEvent;

    private readonly BattleField battleField;
    private GameState currentGameState;
    #endregion
    #region Properties
    public GameState CurrentGameState
    {
        get { return currentGameState; }
    }

    public Transform GetGunpoint(HeroType type) //todo: add the enemy gunpoint
    {
        var hero = battleField.GetHero(type);
        var hand = hero.Hand;
        return hand.GunPoint;
    }
    #endregion
    #region Public methods
    public GameCore(Transform ownHandTransform, Transform enemyHandTransform)
    {
        currentGameState = GameState.Waiting;

        var ownHand = new HandController(ownHandTransform);
        var ownHero = new Hero(HeroType.Own, ownHand, 100, 6);

        var enemyHand = new HandController(enemyHandTransform);
        var enemyHero = new Hero(HeroType.Enemy, enemyHand, 100, 6);

        battleField = new BattleField(ownHero, enemyHero);
    }

    public bool CanShoot()
    {
        var hero = battleField.GetHero(HeroType.Own);
        if (hero.CurrentAmmo <= 0)
        {
            return false;
        }

        hero.CurrentAmmo--;
        if (AmmoChangedEvent != null)
        {
            AmmoChangedEvent(HeroType.Own, hero.CurrentAmmo);
        }
        return true;
    }

    public void StartWaiting() //the logic of punishment
    {
        currentGameState = GameState.Waiting;
        if (GameDoWaitedEvent != null)
        {
            GameDoWaitedEvent();
        }
    }

    public void StartCountdown()
    {
        currentGameState = GameState.Countdown;
        if (GameDoCountdownEvent != null)
        {
            GameDoCountdownEvent();
        }
    }

    public void StartGame()
    {
        currentGameState = GameState.Battle;
        if (GameDoStartedEvent != null)
        {
            GameDoStartedEvent();
        }
    }

    public bool CheckCollision(HeroType type, Vector2 bulletPos)
    {
        var hero = battleField.GetHero(type);
        var gunpointPos = hero.Hand.GunPoint.position;
        var bodyParts = hero.BodyParts;

        int damageCount = CollisionController.CheckCollision(bulletPos, gunpointPos, bodyParts);
        if (damageCount > 0)
        {
            Damage(type, damageCount);
            CheckEndGame();
            return true;
        }

        return false;
    }


    public void RotateHandTo(HeroType type, Vector2 aim)
    {
        Hero hero = battleField.OwnHero;

        hero.Hand.LookTo(aim);
    }
    #endregion
    #region Private methods
    private void CheckEndGame()
    {
        var ownHealth = battleField.OwnHero.CurrentHealth;
        var enemyHealth = battleField.EnemyHero.CurrentHealth;

        var oneHeroIsDead = ownHealth <= 0 || enemyHealth <= 0;
        if (currentGameState == GameState.Battle && oneHeroIsDead)
        {
            currentGameState = GameState.End;
        }
    }


    private void Damage(HeroType type, int damageCount)
    {
        var hero = battleField.GetHero(type);
        hero.CurrentHealth -= damageCount;

        if (HealthChangedEvent != null)
        {
            HealthChangedEvent(type, hero.CurrentHealth);
        }
    }
    #endregion
    #region Event handlers
    public delegate void HealthChangedEventHandler(HeroType type, int health);
    public delegate void AmmoChangedEventHandler(HeroType type, int ammo);
    public delegate void GameDoStartedEventHandler();
    public delegate void GameDoWaitedEventHandler();
    public delegate void GameDoCountdownEventHandler();
    #endregion
}