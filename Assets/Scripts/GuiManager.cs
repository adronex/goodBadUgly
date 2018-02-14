using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour
{
    [SerializeField] private RectTransform aimArenaRect;
    [SerializeField] private RectTransform heroArenaRect;
    [SerializeField] private RectTransform shootArenaRect;
    [SerializeField] private GameObject countdownTimer;
    [SerializeField] private Image EnemyHealth;
    [SerializeField] private Text ownAmmo;

    private const int countdown = 3;

    private Area[] areas;

    private void Awake()
    {
        areas = new Area[3];
        areas[0] = new AimArea(aimArenaRect);
        areas[1] = new HeroArea(heroArenaRect);
        areas[2] = new ShootArea(shootArenaRect);
    }


    private void OnEnable()
    {
        Hero.HealthChangedEvent += UpdateHealth;
        Hero.HeroDeadEvent += DeathNotify;
        Hero.AmmoChangedEvent += UpdateAmmo;
    }


    private void OnDisable()
    {
        Hero.HealthChangedEvent -= UpdateHealth;
        Hero.HeroDeadEvent -= DeathNotify;
        Hero.AmmoChangedEvent -= UpdateAmmo;
    }


    private void Update()
    {
        for (int i = 0; i < areas.Length; i++)
        {
            if (areas[i].Check(Input.mousePosition))
            {
                areas[i].Pressed();
            }
            else
            {
                areas[i].Depressed();
            }
        }
    }


    private void UpdateHealth(int heroId, int currentHealth)
    {
        if (heroId == 0) //ownHero
        {

        }
        else if (heroId == 1) //enemyHero
        {
            EnemyHealth.fillAmount = 0.01f * currentHealth;
        }
    }


    private void DeathNotify(int heroId)
    {
        if (heroId == 0)
        {
            print("Own hero is dead");
        }
        else if (heroId == 1)
        {
            print("Enemy hero is dead");
        }
    }


    private void UpdateAmmo(int heroId, int currentAmmo)
    {
        if (heroId == 0)
        {
            ownAmmo.text = currentAmmo + "/6";
        }
    }

}
