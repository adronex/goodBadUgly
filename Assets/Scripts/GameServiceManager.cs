using System;
using System.Collections;
using UnityEngine;

public class GameServiceManager : MonoBehaviour
{
    private const int countdown = 3;

    private GameService gameService;
    private GameObject ownHeroPrefabInstance;
    private GameObject enemyHeroPrefabInstance;

    private bool isCountdown;
    private bool isRoundStarted;

    private GameObject bulletPrefab;
    private Transform handAxis;
    private Transform gunpoint;


    private void Awake()
    {
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        handAxis = GameObject.Find("OwnHero").transform.Find("HandAxis");
        gunpoint = handAxis.Find("Hand").Find("Gunpoint");

        gameService = new GameService(gunpoint);

        cou = Countdown();
    }


    private void OnEnable()
    {
        AimArea.AimAreaPressedEvent += OnAimAreaTouched;
        HeroArea.HeroAreaPressedEvent += OnHeroTouched;
        ShootArea.ShootAreaPressedEvent += OnShootAreaTouched;
        BodyPart.BodyPartDamagedEvent += BodyPartDamaged;
        HeroArea.HeroAreaDepressedEvent += OnHeroUntouched;
    }


    private void OnDisable()
    {
        AimArea.AimAreaPressedEvent -= OnAimAreaTouched;
        HeroArea.HeroAreaPressedEvent -= OnHeroTouched;
        ShootArea.ShootAreaPressedEvent -= OnShootAreaTouched;
        BodyPart.BodyPartDamagedEvent -= BodyPartDamaged;
        HeroArea.HeroAreaDepressedEvent -= OnHeroUntouched;
    }


    private void Update()
    {
        if (isRoundStarted && Input.GetKeyDown(KeyCode.W) && gameService.TryShoot)
        {
            var angle = handAxis.localRotation.eulerAngles.x;
            //gameService.Shoot(angle);

            Instantiate(bulletPrefab, gunpoint.position, gunpoint.rotation);
        }
    }

    IEnumerator cou;

    public void OnHeroTouched()
    {
        if (!isCountdown && !isRoundStarted)
        {
            StartCoroutine(cou);
        }
    }


    private void OnHeroUntouched()
    {
        if (isCountdown && !isRoundStarted)
        {
            isCountdown = false;
            StopCoroutine(cou);
            cou = Countdown();
        }
    }


    private IEnumerator Countdown()
    {
        isCountdown = true;

        yield return new WaitForSeconds(countdown);
        print("STARTED");
        isRoundStarted = true;
    }
    

    private void BodyPartDamaged(int damage)
    {
        gameService.DamageEnemy(damage);
        print(damage);
    }


    public void OnAimAreaTouched()
    {
    }
    public void OnShootAreaTouched()
    {
    }
}
