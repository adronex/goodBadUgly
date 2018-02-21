using System;
using GameCore;
using GameCore.Model;
using UnityEngine;

namespace GameLogic
{
    public class GameCore
    {
        private readonly BattleField battleField = new BattleField();
        private GameState currentGameState = GameState.Preparing;
        // pregame countdown timer
        // game timer

        public GameCore()
        {
            InitHeroes();
            SetListeners();
        }

        private void InitHeroes()
        {
            battleField.OwnHero.Init(HeroType.Own, health: 100, ammo: 6);
            battleField.EnemyHero.Init(HeroType.Enemy, health: 100, ammo: 6);
        }

        private void SetListeners()
        {
            // battleField.OwnHero.headCollider.OnCollision = new CollistionHandler() { HitHero(Bodypart.Head); } 
            // battleField.OwnHero.bodyCollider.OnCollision = new CollistionHandler() { HitHero(Bodypart.Body); } 
            // battleField.OwnHero.legsCollider.OnCollision = new CollistionHandler() { HitHero(Bodypart.Legs); } 
        }

        //unused
        private void HitHero(BodypartType bodypart)
        {
            switch (bodypart)
            {
                case BodypartType.Head:
                    // headLogic
                    break;
                case BodypartType.Body:
                    // bodyLogic
                    break;
                case BodypartType.Legs:
                    // legsLogic
                    break;
            }
            CheckEndGame();
        }

        public void CheckEndGame()
        {
            if (currentGameState == GameState.Battle)
            {
                if (battleField.OwnHero.CurrentHealth <= 0 || battleField.EnemyHero.CurrentHealth <= 0)
                {
                    currentGameState = GameState.End;
                }
            }
        }

        public bool Shoot(/*int angle*/)
        {
            if (battleField.OwnHero.CurrentAmmo <= 0)
            {
                return false;
            }

            battleField.OwnHero.CurrentAmmo--;
            return true;
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

        public bool CheckCollision(Vector2 bulletPos)
        {
            int damageCount = CollisionController.CheckCollision(bulletPos, battleField.EnemyHero.BodyParts);
            if (damageCount > 0)
            {
                Damage(HeroType.Enemy, damageCount);
                CheckEndGame();
                return true;
            }
            return false;
        }
    }
}