using System;
using GameCore;
using GameCore.Model;

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
            // battlefield.OwnHero.PositionX = randomPosition
            // battlefield.OwnHero.PositionY = randomPosition
            // battlefield.EnemyHero.PositionX = randomPosition
            // battlefield.EnemyHero.PositionY = randomPosition
        }

        private void SetListeners()
        {
            // battleField.OwnHero.headCollider.OnCollision = new CollistionHandler() { HitHero(Bodypart.Head); } 
            // battleField.OwnHero.bodyCollider.OnCollision = new CollistionHandler() { HitHero(Bodypart.Body); } 
            // battleField.OwnHero.legsCollider.OnCollision = new CollistionHandler() { HitHero(Bodypart.Legs); } 
        }

        private void HitHero(Bodypart bodypart)
        {
            switch (bodypart)
            {
                case Bodypart.Head:
                    // headLogic
                    break;
                case Bodypart.Body:
                    // bodyLogic
                    break;
                case Bodypart.Legs:
                    // legsLogic
                    break;
                default:
                    throw new ArgumentOutOfRangeException("bodypart", bodypart, null);
            }
            CheckEndGame();
        }

        private void CheckEndGame()
        {
            // if gamestate == battle
            // if one heroes health <= 0 then
            // gamestate = end
        }

        public void Shoot(int angle)
        {
            if (battleField.OwnHero.CurrentAmmo > 0)
            {
                // reduce ammo
                // create new bullet (2d physics object)
                // add force to bullet
            }
        }
    }
}