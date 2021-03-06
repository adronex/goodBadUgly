﻿using Core;
using Core.Heroes;
using System.Collections;
using UnityEngine;

public class AI : MonoBehaviour
{
    public GameCore GameCore;
    private Hero ownHero;
    private Hero enemyHero;

    private IEnumerator reload;


    private void Update()
    {
        if (GameCore.CurrentGameState == GameState.Battle)
        {
            var ownBody = ownHero.BodyParts[1].Transform.position;
            enemyHero.RotateHand(ownBody);

            if (reload == null)
            {
                enemyHero.Shoot();
                reload = ReloadRoutine();
                StartCoroutine(reload);
            }
        }
    }

    private IEnumerator ReloadRoutine()
    {
        yield return new WaitForSecondsRealtime(2f); //reload
        reload = null;
    }


    public static void CreateAI(GameCore gameCore, Hero ownHero, Hero enemyHero)
    {
        var ai = new GameObject();
        var comp = ai.AddComponent<AI>();
        comp.GameCore = gameCore;
        comp.enemyHero = enemyHero;
        comp.ownHero = ownHero;
    }
}
