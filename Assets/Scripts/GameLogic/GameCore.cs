using GameCore.Model;
using UnityEngine;

namespace GameLogic
{
    public class GameCore
    {
        private readonly BattleField battleField = new BattleField();

        private GameState currentGameState = GameState.Preparing;

        public void CheckEndGame()
        {
            var oneHeroIsDead = battleField.OwnHero.CurrentHealth <= 0 || battleField.EnemyHero.CurrentHealth <= 0;
            if (currentGameState == GameState.Battle && oneHeroIsDead)
            {
                currentGameState = GameState.End;
            }
        }

        public bool CouldShoot( /*int angle*/)
        {
            if (battleField.OwnHero.CurrentAmmo <= 0)
            {
                return false;
            }

            battleField.OwnHero.OnShoot();
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