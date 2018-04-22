using Core;
using System.Collections;
using UnityEngine;

public class AI : MonoBehaviour
{
    public GameCore GameCore;
    private Hero ownHero;
    private Hero enemyHero;


    public static void CreateAI(GameCore gameCore, Hero ownHero, Hero enemyHero)
    {
        var ai = new GameObject();
        var comp = ai.AddComponent<AI>();
        comp.GameCore = gameCore;
        comp.enemyHero = enemyHero;
        comp.ownHero = ownHero;
    }

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
        yield return new WaitForSecondsRealtime(1f);
        reload = null;
    }

    ///delete
    private void Awake()
    {
        instanse = this;
    }
    private static AI instanse; 
    public static void Print(float f)
    {
        instanse.PP(f);
    }
    void PP(float f)
    {
        print(f);
    }

}
