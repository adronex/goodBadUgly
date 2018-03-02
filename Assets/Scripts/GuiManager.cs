using UnityEngine.UI;
using UnityEngine;
using Model;
using System.Collections;

public class GuiManager : MonoBehaviour
{
    #region Fields
    [SerializeField] private Text ownAmmo;
    [SerializeField] private Text enemyAmmo;
    [SerializeField] private Image ownHealth;
    [SerializeField] private Image enemyHealth;
    [SerializeField] private GameObject StartNotification;
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
        GameCore.GameStartedEvent += ShowNotification;
    }


    private void ShowNotification()
    {
        StartNotification.SetActive(true);
        StartCoroutine(HideNotificationRoutine());
    }
    

    private void OnDisable()
    {
        Hero.HealthChangedEvent -= UpdateHealth;
        Hero.AmmoChangedEvent -= UpdateAmmo;
        GameCore.GameStartedEvent -= ShowNotification;
    }
    #endregion
    #region Private methods
    private IEnumerator HideNotificationRoutine()
    {
        yield return new WaitForSeconds(0.3f);

        var image = StartNotification.GetComponent<Image>();
        var newColor = image.color;
        for (float alpha = 1; alpha > 0; alpha -= 0.02f)
        {
            newColor.a = alpha;
            image.color = newColor;
            yield return null;
        }

        StartNotification.SetActive(false);
    }


    private void UpdateAmmo(HeroType hero, int currentAmmo)
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


    private void UpdateHealth(HeroType hero, int currentHealth)
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
    #endregion


    public void HeroDead(HeroType hero)
    {
        print(hero + "is dead");
    }
}