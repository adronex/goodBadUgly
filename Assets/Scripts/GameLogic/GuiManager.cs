using UnityEngine.UI;
using UnityEngine;

public class GuiManager : MonoBehaviour
{
    #region Fields
    [SerializeField] private Text ownAmmo;
    [SerializeField] private Text enemyAmmo;
    [SerializeField] private Image ownHealth;
    [SerializeField] private Image enemyHealth;
    #endregion
    #region Temp
    private string text;
    private float fillAmount;
    #endregion
    #region Unity lifecycle
    private void OnEnable()
    {
        Hero.HealthChangedEvent += UpdateHealth;
        Hero.AmmoChangedEvent += UpdateAmmo;
    }


    private void OnDisable()
    {
        Hero.HealthChangedEvent -= UpdateHealth;
        Hero.AmmoChangedEvent -= UpdateAmmo;
    }
    #endregion
    #region Public methods
    public void UpdateAmmo(HeroType hero, int currentAmmo)
    {
        text = currentAmmo + "/6";
        switch (hero)
        {
            case HeroType.Own:
                ownAmmo.text = text;
                break;
            case HeroType.Enemy:
                enemyAmmo.text = text;
                break;
        }
    }


    public void UpdateHealth(HeroType hero, int currentHealth)
    {
        fillAmount = 0.01f * currentHealth;
        switch (hero)
        {
            case HeroType.Own:
                ownHealth.fillAmount = fillAmount;
                break;
            case HeroType.Enemy:
                enemyHealth.fillAmount = fillAmount;
                break;
        }
    }


    public void HeroDead(HeroType hero)
    {
        print(hero + "is dead");
    }
    #endregion
}