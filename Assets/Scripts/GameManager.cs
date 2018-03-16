using Controller;
using Model;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Fields
    private const string BULLET_PREFAB_PATH = "Prefabs/Bullet";

    [SerializeField] private Transform ownHandAxis;
    [SerializeField] private Transform enemyHandAxis;
    [SerializeField] private RectTransform AimArena;
    [SerializeField] private RectTransform HeroArena;
    [SerializeField] private RectTransform ShootArena;

    private GameCore gameCore;
    private Countdown countdown;
    private Mouse mouse;

    private GameObject bulletPrefab;

    private List<Transform> ownBullets = new List<Transform>();
    private List<Transform> enemyBullets = new List<Transform>();

    #endregion
    #region Unity lifecycle
    void Awake()
    {
        gameCore = new GameCore(ownHandAxis, enemyHandAxis);
        countdown = new Countdown(gameCore, AimArena, HeroArena, ShootArena);
        mouse = new Mouse();

        bulletPrefab = Resources.Load<GameObject>(BULLET_PREFAB_PATH);
    }


    private void OnEnable()
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
            CreateBullet(HeroType.Own);
        }

        if (mouse.IsLooking)
        {
            var mousePos = Input.mousePosition;
            var gameMousePos = Camera.main.ScreenToWorldPoint(mousePos);
            gameCore.RotateHandTo(HeroType.Own, gameMousePos);
        }

        if (ownBullets.Count > 0)
        {
            DestroyCollidedBullets(ownBullets, HeroType.Enemy);
        }

        if (enemyBullets.Count > 0)
        {
            DestroyCollidedBullets(enemyBullets, HeroType.Own);
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
    public void CreateBullet(HeroType type)
    {
        Transform gunpoint = gameCore.GetGunpoint(type);

        var bullet = Instantiate(bulletPrefab, gunpoint.position, gunpoint.rotation);
        ownBullets.Add(bullet.transform);
    }
    #endregion
    #region Private methods
    private void DestroyCollidedBullets(List<Transform> bullets, HeroType type)
    {
        for (int index = 0; index < bullets.Count; index++)
        {
            var bullet = bullets[index];

            var isCollided = gameCore.CheckCollision(type, bullet.position);
            if (isCollided)
            {
                Destroy(bullet.gameObject);
                bullets.Remove(bullet);
                //////////////////////todo: DELETE THIS
                var jj = Random.Range(0, 1);
                string paramName = asdas == false ? "fall_forward" : "back_step";
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
        if (gameCore.CurrentGameState == GameState.Waiting)
        {
            gameCore.StartCountdown();

            var newCountdown = countdown.CreateNew();
            StartCoroutine(newCountdown);
        }
    }


    private void StopCountdown()
    {
        if (gameCore.CurrentGameState == GameState.Countdown)
        {
            StopCoroutine(countdown.Current);

            gameCore.StartWaiting();
        }
    }
    #endregion
}
