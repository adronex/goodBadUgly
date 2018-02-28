using Controller;
using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Fields
    [SerializeField] private Transform ownHandAxis;
    [SerializeField] private Transform enemyHandAxis;
    [SerializeField] private RectTransform AimArena;
    [SerializeField] private RectTransform HeroArena;
    [SerializeField] private RectTransform ShootArena;

    const float coundownTimer = 3f;

    private GameCore gameCore;

    private Area aimArena;
    private Area heroArena;
    private Area shootArena;

    private GameObject bulletPrefab;
    private List<Transform> ownBullets = new List<Transform>();
    private List<Transform> enemyBullets = new List<Transform>();

    private bool isLookToMouse;
    private IEnumerator countdown;
    #endregion
    #region Unity lifecycle
    void Awake()
    {
        gameCore = new GameCore(ownHandAxis, enemyHandAxis);

        aimArena = new Area(AreaType.AimArea, AimArena);
        heroArena = new Area(AreaType.HeroArea, HeroArena);
        shootArena = new Area(AreaType.ShootArea, ShootArena);

        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
    }


    private void OnEnable()
    {
        Area.AimArenaEnterEvent += StartLook;
        Area.AimArenaExitEvent += FinishLook;
        Area.HeroArenaEnterEvent += StartCountdown;
        Area.HeroArenaExitEvent += StopCountdown;
    }


    void Update()
    {
        switch (gameCore.CurrentGameState)
        {
            case GameState.Waiting:
                heroArena.Invoke(Input.mousePosition);
                return;
            case GameState.Countdown:
                gameCore.StartCountdown();
                heroArena.Invoke(Input.mousePosition);
                return;
            case GameState.Battle:
                aimArena.Invoke(Input.mousePosition);
                shootArena.Invoke(Input.mousePosition);
                break;
            case GameState.End:
                print("you win!");
                break;
            default: throw new UnityException();
        }

        if (Input.GetKeyDown(KeyCode.W) && gameCore.CouldShoot)
        {
            CreateBullet(HeroType.Own);
        }

        if (isLookToMouse)
        {
            var gameMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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


        //if (gameCore.CurrentGameState == GameState.Waiting)
        //{
        //    heroArena.Invoke(Input.mousePosition);
        //    return;
        //}

        //if (gameCore.CurrentGameState == GameState.Countdown)
        //{
        //    gameCore.StartCountdown();

        //    heroArena.Invoke(Input.mousePosition);
        //    return;
        //}

        //if (gameCore.CurrentGameState == GameState.Battle)
        //{
        //    aimArena.Invoke(Input.mousePosition);
        //    shootArena.Invoke(Input.mousePosition);
        //}

        //if (Input.GetKeyDown(KeyCode.W) && gameCore.CouldShoot)
        //{
        //    CreateBullet(HeroType.Own);
        //}

        //if (isLookToMouse)
        //{
        //    var gameMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    gameCore.RotateHandTo(HeroType.Own, gameMousePos);
        //}

        //if (ownBullets.Count > 0)
        //{
        //    DestroyCollidedBullets(ownBullets, HeroType.Enemy);
        //}

        //if (enemyBullets.Count > 0)
        //{
        //    DestroyCollidedBullets(enemyBullets, HeroType.Own);
        //}
    }


    private void OnDisable()
    {
        Area.AimArenaEnterEvent -= StartLook;
        Area.AimArenaExitEvent -= FinishLook;
    }
    #endregion

    #region Public methods
    public void CreateBullet(HeroType type)
    {
        Transform gunpoint;
        switch (type)
        {
            case HeroType.Own:
                gunpoint = gameCore.OwnGunpoint;
                break;
            case HeroType.Enemy:
                gunpoint = gameCore.EnemyGunpoint;
                break;
            default:
                throw new UnityException();
        }

        var bullet = Instantiate(bulletPrefab, gunpoint.position, gunpoint.rotation);
        ownBullets.Add(bullet.transform);
    }
    #endregion

    #region Private methods
    private void StopCountdown()
    {
        if (gameCore.CurrentGameState == GameState.Countdown)
        {
            StopCoroutine(countdown);
            gameCore.StartWaiting();
        }
    }


    private void StartCountdown()
    {
        if (gameCore.CurrentGameState == GameState.Waiting)
        {
            gameCore.StartCountdown();
            countdown = StartCountdownRoutine();
            StartCoroutine(countdown);
        }
    }


    private IEnumerator StartCountdownRoutine()
    {
        yield return new WaitForSeconds(3f);

        gameCore.StartGame();
    }


    private void StartLook()
    {
        if (!isLookToMouse)
        {
            isLookToMouse = true;
        }
    }


    private void FinishLook()
    {
        if (isLookToMouse)
        {
            isLookToMouse = false;
        }
    }


    private void DestroyCollidedBullets(List<Transform> bullets, HeroType type)
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            var bullet = bullets[i];
            if (gameCore.CheckCollision(type, bullet.position))
            {
                Destroy(bullet.gameObject);
                bullets.Remove(bullet);
            }
        }
    }
    #endregion
}