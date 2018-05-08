using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip shoot;
    [SerializeField] private AudioClip hurt;
    [SerializeField] [Range(0, 1)] private float shootVolume;
    [SerializeField] [Range(0, 1)] private float hurtVolume;

    private static AudioManager instanse;

    private AudioSource ownSource;
    private AudioSource enemySource;

    private void Awake()
    {
        instanse = this;
    }


    private void Start()
    {
        ownSource = GameObject.Find("OwnCowboy").GetComponent<AudioSource>();
        enemySource = GameObject.Find("EnemyCowboy").GetComponent<AudioSource>();
    }


    public static void Shoot(Hero hero)
    {
        if (hero as OwnHero != null)
        {
            instanse.OwnShoot();
        }
        else if (hero as EnemyHero != null)
        {
            instanse.EnemyShoot();
        }
    }

    private void OwnShoot()
    {
        if (ownSource.volume != shootVolume)
        {
            ownSource.volume = shootVolume;
        }

        ownSource.PlayOneShot(shoot);
    }

    private void EnemyShoot()
    {
        if (enemySource.volume != shootVolume)
        {
            enemySource.volume = shootVolume;
        }

        enemySource.PlayOneShot(shoot);
    }

    public void HeroHurt(Transform hero)
    {
        var source = hero.GetComponent<AudioSource>();

        if (enemySource.volume != hurtVolume)
        {
            enemySource.volume = hurtVolume;
        }

        source.PlayOneShot(hurt);
    }

    public static void Hurt(Transform collision)
    {
        var hero = GetRootObject(collision);
        instanse.HeroHurt(hero);
    }

    private static Transform GetRootObject(Transform collision)
    {
        var parent = collision.parent;
        if (parent == null)
        {
            return collision;
        }

        return GetRootObject(parent);
    }
}
