using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public AreaController Area;
    public HandController Hand;
    public BodyPartContoller BodyPart;
    public BulletController Bullet;

    public List<Transform> bullets = new List<Transform>(); //using.System.* must be removed cuz it weights ~ 2 mb memory
    private GameObject bulletPrefab;
    private Transform handAxis;
    private Transform gunpoint;


    public void OnNotification(Notification notification)
    {
        switch (notification)
        {
            case Notification.AimAreaEnter:
                Hand.LookToMouse();
                break;
        }
    }


    public void Shoot()
    {
        App.Model.OwnHero.CurrentAmmo--;
        App.View.Gui.UpdateAmmo(Hero.Own, App.Model.OwnHero.CurrentAmmo);
        bullets.Add(Instantiate(bulletPrefab, gunpoint.position, gunpoint.rotation).transform);
    }


    public void Damage(int damage)
    {
        App.Model.OwnHero.CurrentHealth -= damage;

        App.View.Gui.UpdateHealth(Hero.Enemy, App.Model.OwnHero.CurrentHealth);

        if (App.Model.OwnHero.CurrentHealth <= 0)
        {
            App.View.Gui.HeroDead(Hero.Enemy);
        }
    }


    private void Awake()
    {
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        handAxis = GameObject.Find("OwnHero").transform.Find("HandAxis");
        gunpoint = handAxis.Find("Hand").Find("Gunpoint");
    }
    

    private void Start()
    {
        Area = new AreaController();
        Hand = new HandController(handAxis);
        BodyPart = new BodyPartContoller();
        Bullet = new BulletController();

        BodyPart.FindEnemyParts();
    }
    

    private void Update()
    {
        if (App.Model.EnemyHero.BodyParts == null)
        {
            return;
        }

        for (int i = 0; i < bullets.Count; i++)
        {
            if (bullets[i] == null)
            {
                continue;
            }

            var bulletPos = bullets[i].position;
            for (int j = 0; j < App.Model.EnemyHero.BodyParts.Length; j++)
            {
                var points = BodyPart.GetPoints(App.Model.EnemyHero.BodyParts[j]);

                if (Bullet.IsBelongToHeroPart(bullets[i].position, points))
                {
                    Destroy(bullets[i].gameObject);
                    Damage(Random.Range(App.Model.EnemyHero.BodyParts[j].MinDamage, App.Model.EnemyHero.BodyParts[j].MaxDamage));
                    App.View.Print(App.Model.EnemyHero.BodyParts[j].Transform.name);
                }
            }
        }
    }
}