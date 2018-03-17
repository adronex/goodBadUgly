using Controller;
using System.Collections.Generic;
using UnityEngine;

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
        countdown.Produce();

        if (Input.GetKeyDown(KeyCode.W) && gameCore.CanShoot())
        {
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

            var isCollided = gameCore.CheckCollision(hero, bullet.position);
            if (isCollided)
            {
                Destroy(bullet.gameObject);
                bullets.Remove(bullet);
                //////////////////////todo: DELETE THIS
                var jj = Random.Range(0, 1);
                var paramName = asdas == false ? "fall_forward" : "back_step";
                asdas = true;
                print(paramName);
                GameObject.Find("EnemyHero").GetComponent<Animator>().SetTrigger(paramName);
            }
        }
    }

    //delete
    private bool asdas;

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
