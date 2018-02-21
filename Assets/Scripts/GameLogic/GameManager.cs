using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Fields

    private GameLogic.GameCore gameCore;
    private HandController hand;
    private Area[] areas;

    //todo
    public static Transform ownGunPoint;
    private GameObject bulletPrefab;
    private List<Transform> bullets = new List<Transform>();

    private bool isLookToMouse;

    #endregion

    #region Unity lifecycle
    void Awake()
    {
        gameCore = new GameLogic.GameCore();

        hand = new HandController(GameObject.Find("OwnHero").transform.Find("HandAxis"));

        areas = new[]
        {
            new Area(AreaType.AimArea, GameObject.Find("AimArena").GetComponent<RectTransform>()),
            new Area(AreaType.HeroArea, GameObject.Find("HeroArena").GetComponent<RectTransform>()),
            new Area(AreaType.ShootArea, GameObject.Find("ShootArena").GetComponent<RectTransform>()),
        };

        ownGunPoint = GameObject.Find("OwnHero").transform.Find("HandAxis").Find("Hand").Find("Gunpoint");
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
    }


    private void OnEnable()
    {
        Area.AimArenaEnterEvent += StartLook;
        Area.AimArenaExitEvent += FinishLook;
    }


    void Update()
    {
        foreach (var area in areas)
        {
            area.Invoke(Input.mousePosition);
        }

        if (Input.GetKeyDown(KeyCode.W) && gameCore.CouldShoot())
        {
            CreateBullet();
        }

        if (isLookToMouse)
        {
            hand.LookToMouse();
        }

        if (bullets.Count > 0)
        {
            foreach (var bullet in bullets)
            {
                if (gameCore.CheckCollision(bullet.position))
                {
                    Destroy(bullet.gameObject);
                    bullets.Remove(bullet);
                    break;
                }
            }
        }
    }


    private void OnDisable()
    {
        Area.AimArenaEnterEvent -= StartLook;
        Area.AimArenaExitEvent -= FinishLook;
    }

    #endregion

    #region Public methods

    public void CreateBullet()
    {
        var bullet = Instantiate(bulletPrefab, ownGunPoint.position, ownGunPoint.rotation);
        bullets.Add(bullet.transform);
    }


    public void Destroy(Transform transform)
    {
        Destroy(transform.gameObject);
    }

    #endregion

    #region Private methods

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

    #endregion
}