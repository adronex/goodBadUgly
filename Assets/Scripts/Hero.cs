using Controller;
using UnityEngine;
using UnityEngine.UI;

public abstract class Hero
{
    private const float ONE_PERCENT = 0.01f;

    protected int maxHp;
    protected int currentHp;
    protected int maxAmmo;
    protected int currentAmmo;

    protected HandController hand;
    protected BodyPart[] bodyParts;

    protected Text ammoText;
    protected Image hpImage;
    protected Transform handAxis;

    public Transform Gunpoint
    {
        get { return hand.GunPoint; }
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

    public void ReduceAmmo()
    {
        currentAmmo--;
        UpdateAmmo();
    }

    public void ReduceHp(int amount)
    {
        currentHp -= amount;
        UpdateHp();
    }

    private void UpdateAmmo()
    {
        ammoText.text = currentAmmo + "/" + maxAmmo;
    }

    private void UpdateHp()
    {
        hpImage.fillAmount = currentHp * ONE_PERCENT;
    }

    protected Hero(int startHp, int startAmmo)
    {
        maxHp = startHp;
        currentHp = startHp;
        maxAmmo = startAmmo;
        currentAmmo = startAmmo;

    }

    public void RotateHand(Vector2 aim)
    {
        hand.LookTo(aim);
    }
}
