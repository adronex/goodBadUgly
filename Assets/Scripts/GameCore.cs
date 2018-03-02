using System;
using Controller;
using Model;
using UnityEngine;

public class GameCore
{
    #region Fields
    private readonly BattleField battleField;
    private GameState currentGameState;
    #endregion
    public delegate void GameStartedEventHandler();
    public static event GameStartedEventHandler GameStartedEvent;
    #region Properties
    public GameState CurrentGameState
    {
        get
        {
            return currentGameState;
        }
    }

    public Transform OwnGunpoint
    {
        get
        {
            return battleField.OwnHero.Hand.GunPoint;
        }
    }

    public Transform EnemyGunpoint
    {
        get
        {
            return battleField.EnemyHero.Hand.GunPoint;
        }
    }

    public bool CouldShoot
    {
        get
        {
            if (battleField.OwnHero.CurrentAmmo <= 0)
            {
                return false;
            }

            battleField.OwnHero.OnShoot();
            return true;
        }
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

    public void StartWaiting() //the logic of punishment
    {
        currentGameState = GameState.Waiting;
    }

    public void StartCountdown()
    {
        currentGameState = GameState.Countdown;
    }

    public void StartGame()
    {
        currentGameState = GameState.Battle;
        if (GameStartedEvent != null)
        {
            GameStartedEvent();
        }
    }

    public bool CheckCollision(HeroType type, Vector2 bulletPos)
    {
        Hero hero;
        switch (type)
        {
            case HeroType.Own:
                hero = battleField.OwnHero;
                break;
            case HeroType.Enemy:
                hero = battleField.EnemyHero;
                break;
            default:
                throw new ArgumentException();
        }

        int damageCount = CollisionController.CheckCollision(bulletPos, hero);
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
        Hero hero;
        switch (type)
        {
            case HeroType.Own:
                hero = battleField.OwnHero;
                aim = hero.Hand.Position - aim;
                break;
            case HeroType.Enemy:
                hero = battleField.EnemyHero;
                break;
            default:
                throw new ArgumentException();
        }

        hero.Hand.LookTo(aim);
    }
    #endregion
    #region Private methods
    private void CheckEndGame()
    {
        var oneHeroIsDead = battleField.OwnHero.CurrentHealth <= 0 || battleField.EnemyHero.CurrentHealth <= 0;
        if (currentGameState == GameState.Battle && oneHeroIsDead)
        {
            currentGameState = GameState.End;
        }
    }


    private void Damage(HeroType type, int damageCount)
    {
        switch (type)
        {
            case HeroType.Own:
                battleField.OwnHero.CurrentHealth -= damageCount;
                break;
            case HeroType.Enemy:
                battleField.EnemyHero.CurrentHealth -= damageCount;
                break;
        }
    }
    #endregion
}