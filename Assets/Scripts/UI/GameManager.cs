using Controller;
using UnityEngine;
using System;
using System.Collections;

public class GameManager : MonoBehaviour
{
    #region Fields
    [SerializeField] private RectTransform AimArena;
    [SerializeField] private RectTransform HeroArena;
    [SerializeField] private RectTransform ShootArena;

    private Transform ownBullet;
    private Transform enemyBullet;

    private GameCore gameCore;
    private Countdown countdown;
    private Mouse mouse;
    
    private IEnumerator reload;

    private Vector3 gameMousePos;
    private Vector3 mousePosition;
    #endregion

    #region Unity lifecycle
    void Awake()
    {
        var ownHero = CreateOwnHero();
        var ownHeroInfo = ownHero.GetComponent<HeroInfo>();

        var enemyHero = CreateEnemyHero();
        var enemyHeroInfo = enemyHero.GetComponent<HeroInfo>();

        gameCore = new GameCore(ownHeroInfo, enemyHeroInfo);

        countdown = new Countdown(gameCore, AimArena, HeroArena, ShootArena);
        mouse = new Mouse();
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
        mousePosition = Input.mousePosition;

        //if the game is over, disable the hand rotation
        if (countdown.Produce(mousePosition) == GameState.End)
        {
            return;
        }

        if (reload == null && Input.GetKeyDown(KeyCode.W) && gameCore.CanShoot())
        {
            reload = ReloadRoutine();
            StartCoroutine(reload);
            CreateBullet(gameCore.OwnHero);
        }

        //check own bullet to the collision with enemy body parts 
        var ownHero = gameCore.OwnHero;
        DestroyCollidedBullets(ref ownBullet, ownHero.PreviousBulletPos, ownHero.CurrentBulletPos, gameCore.EnemyHero);

        ownHero.MoveBullet();

        //check enemy bullets to the collisions with own body parts
        //DestroyCollidedBullets(ref enemyBullet, gameCore.OwnHero);
    }


    private void LateUpdate()
    {
        //if the game is over, disable the hand rotation
        if (gameCore.CurrentGameState == GameState.End)
        {
            return;
        }

        //if the targeting is available, update the targeting position
        if (mouse.IsLooking)
        {
            gameMousePos = Camera.main.ScreenToWorldPoint(mousePosition);
        }

        gameCore.RotateHand(gameCore.OwnHero, gameMousePos);
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
        var bullet = hero.Shoot();
        ownBullet = bullet.transform;
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



    private void DestroyCollidedBullets(ref Transform bullet, Vector2 previous, Vector2 current, Hero hero)
    {
        if (bullet == null)
        {
            return;
        }

        if (bullet.position.x > 4)//TOOOOOOOOOOOOODDDDDDDDDDDDDDDDDDDDDDDOOOOOOOOOOOOOOOOOOOOOOO
        {
        }

        var isCollided = gameCore.CheckCollision(hero, previous, current);
        if (isCollided)
        {
            Destroy(bullet.gameObject);
            bullet = null;
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


    private IEnumerator ReloadRoutine()
    {
        yield return new WaitForSecondsRealtime(0.7f);
        reload = null;
    }
    #endregion
}

