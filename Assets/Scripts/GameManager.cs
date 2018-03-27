using Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    #region Fields
    private const string BULLET_PREFAB_PATH = "Prefabs/Bullet";

    [SerializeField] private RectTransform AimArena;
    [SerializeField] private RectTransform HeroArena;
    [SerializeField] private RectTransform ShootArena;

    private GameCore gameCore;
    private Countdown countdown;
    private Mouse mouse;

    private GameObject bulletPrefab;

    private readonly List<Transform> ownBullets = new List<Transform>();
    private readonly List<Transform> enemyBullets = new List<Transform>();

    private IEnumerator reload;
    #endregion
    #region Unity lifecycle
    void Awake()
    {
        var heroesManager = FindObjectOfType<HeroesManager>();
        gameCore = new GameCore(heroesManager);

        countdown = new Countdown(gameCore, AimArena, HeroArena, ShootArena);
        mouse = new Mouse();

        bulletPrefab = Resources.Load<GameObject>(BULLET_PREFAB_PATH);
    }


    void OnEnable()
    {
        Area.AimArenaEnterEvent += mouse.StartLook;
        Area.AimArenaExitEvent += mouse.StopLook;
        Area.HeroArenaEnterEvent += StartCountdown;
        Area.HeroArenaExitEvent += StopCountdown;
    }


    void Update()
    {
        if (countdown.Produce() == GameState.End)
        {
            return;
        }

        if (reload == null && Input.GetKeyDown(KeyCode.W) && gameCore.CanShoot())
        {
            reload = ReloadRoutine();
            StartCoroutine(reload);
            CreateBullet(gameCore.OwnHero);
        }

        if (mouse.IsLooking)
        {
            var mousePos = Input.mousePosition;
            var gameMousePos = Camera.main.ScreenToWorldPoint(mousePos);
            gameCore.RotateHand(gameCore.OwnHero, gameMousePos);
        }

        if (ownBullets.Count > 0)
        {
            DestroyCollidedBullets(ownBullets, gameCore.EnemyHero);
        }

        if (enemyBullets.Count > 0)
        {
            DestroyCollidedBullets(enemyBullets, gameCore.OwnHero);
        }
    }

    private IEnumerator ReloadRoutine()
    {
        yield return new WaitForSecondsRealtime(0.7f);
        reload = null;
    }

    private void OnDisable()
    {
        Area.AimArenaEnterEvent -= mouse.StartLook;
        Area.AimArenaExitEvent -= mouse.StopLook;
        Area.HeroArenaEnterEvent -= StartCountdown;
        Area.HeroArenaExitEvent -= StopCountdown;
    }
    #endregion
    #region Public methods
    public void CreateBullet(Hero hero)
    {
        var gunpoint = hero.Gunpoint;

        var bullet = Instantiate(bulletPrefab, gunpoint.position, gunpoint.rotation);
        ownBullets.Add(bullet.transform);
    }
    #endregion
    #region Private methods
    private void DestroyCollidedBullets(List<Transform> bullets, Hero hero)
    {
        for (var index = 0; index < bullets.Count; index++)
        {
            var bullet = bullets[index];

            var isCollided = gameCore.CheckCollision(hero, bullet);
            if (isCollided)
            {
                Destroy(bullet.gameObject);
                bullets.Remove(bullet);

            }
        }
    }


    private void StartCountdown()
    {
        if (gameCore.CurrentGameState != GameState.Waiting) return;

        gameCore.StartCountdown();

        var newCountdown = countdown.CreateNew();
        StartCoroutine(newCountdown);
    }


    private void StopCountdown()
    {
        if (gameCore.CurrentGameState != GameState.Countdown) return;

        StopCoroutine(countdown.Current);

        gameCore.StartWaiting();
    }
    #endregion
}
