using Controller;
using UnityEngine;
using UnityEngine.UI;

public abstract class Hero
{
    #region Fields
    private const float ONE_PERCENT = 0.01f;

    protected int maxHp;
    protected int currentHp;
    protected int maxAmmo;
    protected int currentAmmo;

    protected HandController hand;
    protected BodyPart[] bodyParts;
    protected Animator animator;

    protected Text ammoText;
    protected Image hpImage;
    protected Transform handAxis;
    #endregion
    #region Properties
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
    #endregion
    #region Public Methods
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
    #endregion
    #region Private Methods
    protected Hero(int startHp, int startAmmo, Animator animator)
    {
        maxHp = startHp;
        currentHp = startHp;
        maxAmmo = startAmmo;
        currentAmmo = startAmmo;

        this.animator = animator;
    }

    private void UpdateAmmo()
    {
        ammoText.text = currentAmmo + "/" + maxAmmo;
    }


    private void UpdateHp()
    {
        hpImage.fillAmount = currentHp * ONE_PERCENT;
    }
    #endregion
}
