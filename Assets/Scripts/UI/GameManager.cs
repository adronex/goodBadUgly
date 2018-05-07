using Controller;
using UnityEngine;
using System;
using System.Collections;
using Core;

namespace UI
{
    public class GameManager : MonoBehaviour
    {
        #region Fields
        [SerializeField] private RectTransform aimArenaRect;
        [SerializeField] private RectTransform heroArenaRect;
        [SerializeField] private RectTransform shootArenaRect;

        private const float COUNTDOWN_TIMER = 1f;//must be changed!

        private GameCore gameCore;

        private Hero ownHero;
        private Hero enemyHero;

        private IEnumerator reload;
        private IEnumerator countdown;

        private Area aimArena;
        private Area heroArena;
        private Area shootArena;

        private Vector3 rotateTo;
        #endregion

        #region Unity lifecycle
        void Awake()
        {

            gameCore = new GameCore();

            var ownHeroObject = CreateOwnHero();
            ownHero = gameCore.LoadOwnHero(ownHeroObject);

            var enemyHeroObject = CreateEnemyHero();
            enemyHero = gameCore.LoadEnemyHero(enemyHeroObject);

            AI.CreateAI(gameCore, ownHero, enemyHero);

            aimArena = new Area(aimArenaRect);
            heroArena = new Area(heroArenaRect);
            shootArena = new Area(shootArenaRect);
        }


        void Update()
        {
            if (gameCore.CurrentGameState == GameState.End)
            {
                return;
            }

            aimArena.ResetTouch();
            heroArena.ResetTouch();
            shootArena.ResetTouch();

#if UNITY_EDITOR
            var touches = new[]
            {
                new Touch()
                {
                   position = Input.mousePosition
                }
            };
#elif UNITY_ANDROID
        var touchCount = Input.touchCount;
        if (touchCount == 0)
        {
            return;
        }

        var touches = Input.touches;
#endif

            Vector2 touchPos;
            foreach (var touch in touches)
            {
                touchPos = touch.position;

                if (aimArena.IsTouched(touchPos) && gameCore.CurrentGameState == GameState.Battle)
                {
                    rotateTo = (Vector2)Camera.main.ScreenToWorldPoint(touchPos);
                }

                if (heroArena.IsTouched(touchPos))
                {
                    StartCountdown();
                }
                else
                {
                    StopCountdown();
                }
                bool isShootArenaTouched = false;

#if UNITY_EDITOR
                if (Input.GetKeyDown(KeyCode.W))
                {
                    isShootArenaTouched = true;
                }
#elif UNITY_ANDROID
            if (shootArena.IsTouched(touchPos))
            {
                isShootArenaTouched = true;
            }
#endif

                if (isShootArenaTouched)
                {
                    if (reload == null && gameCore.CurrentGameState == GameState.Battle && ownHero.Shoot())
                    {
                        reload = ReloadRoutine();
                        StartCoroutine(reload);
                    }

                    if (reload == null && gameCore.CurrentGameState == GameState.Countdown)
                    {
                        ownHero.PlayFalseStart();
                        print("BB");
                    }

                }
            }
        }


        private void LateUpdate()
        {
            gameCore.MoveBullets();
            gameCore.CheckCollisions();

            gameCore.CheckGameDraw();

            ownHero.RotateHand(rotateTo);
        }
        #endregion
        #region Private methods
        private GameObject CreateOwnHero()
        {
            var prefab = LoadHero(OwnHero.heroType);

            var hero = Instantiate(prefab, new Vector3(-8f, -0.34f), Quaternion.identity);
            hero.name = "OwnCowboy";

            return hero;
        }


        private GameObject CreateEnemyHero()
        {
            var prefab = LoadHero(EnemyHero.heroType);

            var hero = Instantiate(prefab, new Vector3(4f, -0.34f), Quaternion.Euler(0, 180, 0));
            hero.name = "EnemyCowboy";

            return hero;
        }


        private GameObject LoadHero(HeroType heroType)
        {
            return Resources.Load<GameObject>(String.Format("Prefabs/Heroes/{0}", heroType.ToString()));
        }


        private void StartCountdown()
        {
            if (gameCore.CurrentGameState != GameState.Waiting) return;

            gameCore.StartCountdown();

            countdown = StartCountdownRoutine();
            StartCoroutine(countdown);
        }


        private void StopCountdown()
        {
            if (gameCore.CurrentGameState != GameState.Countdown) return;

            StopCoroutine(countdown);

            gameCore.StartWaiting();
        }


        private IEnumerator ReloadRoutine()
        {
            yield return new WaitForSecondsRealtime(0.05f);
            reload = null;
        }


        private IEnumerator StartCountdownRoutine()
        {
            yield return new WaitForSeconds(COUNTDOWN_TIMER);

            gameCore.StartGame();
        }
        #endregion
    }
}