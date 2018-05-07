using Controller;
using UI;
using UnityEngine;

namespace Core
{
    public class GameCore
    {
        #region Fields
        public static event GameStartingEventHandler GameStartingEvent;
        public static event GameWaitingEventHandler GameWaitingEvent;
        public static event StartingCountdownEventHandler StartingCountdownEvent;
        public static event GameEndingEventHandler GameEndingEvent;
        public static event GameDrawEventHandler GameDrawEvent;

        private Hero ownHero;
        private Hero enemyHero;

        private NetworkBridge networkBridge;
        #endregion
        #region Properties
        public GameState CurrentGameState { get; private set; }

        public Transform GetGunpoint(Hero hero)
        {
            return hero.Gunpoint;
        }

        #endregion
        #region Public methods
        public GameCore()
        {
            CurrentGameState = GameState.Waiting;
        }

        public Hero LoadOwnHero(GameObject ownHeroObject)
        {
            var heroInfo = ownHeroObject.GetComponent<HeroInfo>();
            ownHero = new OwnHero(heroInfo);
            return ownHero;
        }

        public Hero LoadEnemyHero(GameObject enemyHeroObject)
        {
            var heroInfo = enemyHeroObject.GetComponent<HeroInfo>();
            enemyHero = new EnemyHero(heroInfo);
            return enemyHero;
        }


        public void MoveBullets()
        {
            ownHero.MoveBullets();
            enemyHero.MoveBullets();
        }


        public void CheckCollisions()
        {
            CheckCollision(ownHero, enemyHero);
            CheckCollision(enemyHero, ownHero);
        }


        public void StartWaiting()
        {
            CurrentGameState = GameState.Waiting;
            AI.Print(CurrentGameState);

            if (GameWaitingEvent != null)
            {
                GameWaitingEvent();
            }
        }


        public void StartCountdown()
        {
            CurrentGameState = GameState.Countdown;
            AI.Print(CurrentGameState);

            if (StartingCountdownEvent != null)
            {
                StartingCountdownEvent();
            }
        }


        public void StartGame()
        {
            CurrentGameState = GameState.Battle;
            AI.Print(CurrentGameState);

            if (GameStartingEvent != null)
            {
                GameStartingEvent();
            }
        }
        #endregion
        #region Private Methods
        private bool CheckCollision(Hero shooter, Hero victim)
        {
            var bullets = shooter.GetBullets;

            for (int i = 0; i < bullets.Length; i++)
            {
                var bullet = bullets[i];
                if (bullet == null)
                {
                    return false;
                }

                var previous = bullet.PreviousPosition;
                var current = bullet.CurrentPosition;

                var bodyParts = victim.BodyParts;
                var bodyPartId = CollisionController.GetCollidedBodyPartId(previous, current, bodyParts);
                if (bodyPartId == -1)
                {
                    return false;
                }

                var bodyPart = bodyParts[bodyPartId];
                victim.Damage(bodyPart.Damage);


                victim.PlayAnimation(bullet, bodyPartId);

                if (victim.IsDead)
                {
                    CurrentGameState = GameState.End;
                    AI.Print(CurrentGameState);


                    if (GameEndingEvent != null)
                    {
                        GameEndingEvent();
                    }
                }

                bullets[i] = null;
            }

            return true;
        }

        public void CheckGameDraw()
        {
            var anyCantShoot = !ownHero.CanShoot && !enemyHero.CanShoot;
            if (anyCantShoot)
            {
                var bulletContainer = ownHero.GetBullets;
                for (int i = 0; i < bulletContainer.Length; i++)
                {
                    if (bulletContainer[i] != null)
                    {
                        return;
                    }
                }

                bulletContainer = enemyHero.GetBullets;
                for (int i = 0; i < bulletContainer.Length; i++)
                {
                    if (bulletContainer[i] != null)
                    {
                        return;
                    }
                }

                if (GameDrawEvent != null)
                {
                    GameDrawEvent();
                }
            }

        }


        #endregion
        #region Event handlers
        public delegate void GameStartingEventHandler();
        public delegate void GameWaitingEventHandler();
        public delegate void StartingCountdownEventHandler();
        public delegate void GameEndingEventHandler();
        public delegate void GameDrawEventHandler();
        #endregion
    }
}
