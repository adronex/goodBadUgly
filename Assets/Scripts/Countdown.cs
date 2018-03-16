using Controller;
using System.Collections;
using UnityEngine;

class Countdown
{
    private const float COUNTDOWN_TIMER = 3f;//must be changed!

    private GameCore gameCore;

    private Area aimArena;
    private Area heroArena;
    private Area shootArena;

    private IEnumerator countdown;

    public IEnumerator Current { get { return countdown; } }

    public Countdown(GameCore gameCore, RectTransform aimRect, RectTransform heroRect, RectTransform shootRect)
    {
        this.gameCore = gameCore;

        aimArena = new Area(AreaType.AimArea, aimRect);
        heroArena = new Area(AreaType.HeroArea, heroRect);
        shootArena = new Area(AreaType.ShootArea, shootRect);
    }

    public void Produce()
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
                //////////////////////////////////////////////////////////////////////////////////////////////////
                break;
            default: throw new UnityException();
        }
    }

    public IEnumerator CreateNew()
    {
        countdown = StartCountdownRoutine();
        return countdown;
    }

    private IEnumerator StartCountdownRoutine()
    {
        yield return new WaitForSeconds(COUNTDOWN_TIMER);

        gameCore.StartGame();
    }
}
