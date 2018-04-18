using Controller;
using System.Collections;
using UnityEngine;

class Countdown
{
    private const float COUNTDOWN_TIMER = 0f;//must be changed!

    private readonly GameCore gameCore;

    private readonly Area aimArena;
    private readonly Area heroArena;
    private readonly Area shootArena;

    public IEnumerator Current { get; private set; }

    public Countdown(GameCore gameCore, RectTransform aimRect, RectTransform heroRect, RectTransform shootRect)
    {
        this.gameCore = gameCore;

        aimArena = new Area(AreaType.AimArea, aimRect);
        heroArena = new Area(AreaType.HeroArea, heroRect);
        shootArena = new Area(AreaType.ShootArea, shootRect);
    }

    public GameState Produce(Vector3 mousePos)
    {
        var currentState = gameCore.CurrentGameState;
        switch (currentState)
        {
            case GameState.Waiting:
                heroArena.Invoke(mousePos);
                break;
            case GameState.Countdown:
                gameCore.StartCountdown();
                heroArena.Invoke(mousePos);
                break;
            case GameState.Battle:
                aimArena.Invoke(mousePos);
                shootArena.Invoke(mousePos);
                break;
            case GameState.End:
                break;
        }
        return currentState;
    }

    public IEnumerator CreateNew()
    {
        Current = StartCountdownRoutine();
        return Current;
    }

    private IEnumerator StartCountdownRoutine()
    {
        yield return new WaitForSeconds(COUNTDOWN_TIMER);

        gameCore.StartGame();
    }
}
