using Controller;
using UnityEngine;

public abstract class Hero
{
    #region Fields 
    private static readonly Vector3 GUNPOINT_OFFSET = new Vector3(-0.25f, 0.8f);

    public static event HpChangedEventHandler HpChangedEvent;
    public static event AmmoChangedEventHandler AmmoChangedEvent;

    public delegate void HpChangedEventHandler(Hero hero, int currentHp, int maxHp);
    public delegate void AmmoChangedEventHandler(Hero hero, int currentAmmo, int maxAmmo);

    private const float ONE_PERCENT = 0.01f;

    protected int maxHp;
    protected int currentHp;
    protected int maxAmmo;
    protected int currentAmmo;

    protected Animator animator;
    protected HandController hand;
    protected BodyPart[] bodyParts;
    protected Transform handAxis;

    protected GameObject bulletPrefab;
    protected float bulletSpeed;

    private Vector2 currentBulletPos;
    private Vector2 previousBulletPos;

    private Vector2 bulletDirection;
    #endregion
    #region Properties
    public Transform Gunpoint
    {
        get { return hand.GunPoint; }
    }

    public Vector3 Offset
    {
        get { return Gunpoint.TransformDirection(GUNPOINT_OFFSET); }
    }

    public BodyPart[] BodyParts
    {
        get { return bodyParts; }
    }

    public bool IsDead
    {
        get { return currentHp <= 0; }
    }

    public bool CanShoot
    {
        get { return currentAmmo <= 0; }
    }

    public Vector2 CurrentBulletPos
    {
        get { return currentBulletPos; }
    }

    public Vector2 PreviousBulletPos
    {
        get { return previousBulletPos; }
    }
    #endregion
    #region Public Methods
    public void MoveBullet()
    {
        if (bulletDirection == Vector2.zero)
        {
            return; 
        }

        previousBulletPos = currentBulletPos;
        currentBulletPos += bulletDirection * bulletSpeed * Time.deltaTime;
    }

    public void ReduceAmmo()
    {
        currentAmmo--;

        if (AmmoChangedEvent != null)
        {
            AmmoChangedEvent(this, currentAmmo, maxAmmo);
        }
    }


    public void ReduceHp(int amount)
    {
        currentHp -= amount;
        if (HpChangedEvent != null)
        {
            HpChangedEvent(this, currentHp, maxHp);
        }
    }


    public void RotateHand(Vector2 aim)
    {
        hand.LookTo(aim);
    }


    public void PlayAnimation(int bodyPartId)
    {
        var angle = Random.Range(0, 90);
        animator.SetInteger("BodyPart", bodyPartId);
        animator.SetFloat("Angle", angle);
        animator.SetTrigger("Do");
    }

    public Transform Shoot()
    {
        var bulletTransform = Bullet.CreateBullet(bulletPrefab, bulletSpeed, Gunpoint, Offset, out bulletDirection);

        currentBulletPos = previousBulletPos = bulletTransform.position;

        return bulletTransform;
    }
    #endregion
    #region Private Methods
    protected Hero(HeroInfo heroInfo)
    {
        maxHp = heroInfo.Hp;
        currentHp = maxHp;

        if (HpChangedEvent != null)
        {
            HpChangedEvent(this, maxHp, maxHp);
        }

        maxAmmo = heroInfo.Ammo;
        currentAmmo = maxAmmo;

        if (AmmoChangedEvent != null)
        {
            AmmoChangedEvent(this, maxAmmo, maxAmmo);
        }

        bulletPrefab = heroInfo.BulletPrefab;
        bulletSpeed = heroInfo.BulletSpeed;

        animator = heroInfo.Animator;

        hand = new HandController(heroInfo);

        bodyParts = BodyPart.FindEnemyParts(heroInfo.Areas);

    }
    #endregion
}
