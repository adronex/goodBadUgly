using UnityEngine.UI;
using UnityEngine;
using Model;
using System.Collections;
using System;

public class GuiManager : MonoBehaviour
{
    #region Fields
    private const float ONE_PERCENT = 0.01f;
    private const float NOTIFICATION_HIDING_PER_FRAME = 0.02f;
    private const float NOTIFICATION_HIDING_DELAY = 0.3f;
    private const float START_ALPHA = 1f;
    private const float END_ALPHA = 0f;

    [SerializeField] private Text ownAmmo;
    [SerializeField] private Text enemyAmmo;
    [SerializeField] private Image ownHealth;
    [SerializeField] private Image enemyHealth;
    [SerializeField] private GameObject StartNotification;
    [SerializeField] private Image AimArenaOutline;
    [SerializeField] private Image HeroArenaOutline;
    [SerializeField] private Image ShootArenaOutline;
    #endregion
    #region Unity lifecycle
    private void OnEnable()
    {
        GameCore.HealthChangedEvent += UpdateHealth;
        GameCore.AmmoChangedEvent += UpdateAmmo;
        GameCore.GameDoStartedEvent += ShowNotification;
        GameCore.GameDoCountdownEvent += HidePrompts;
        GameCore.GameDoWaitedEvent += ShowPrompts;
    }


    private void ShowNotification()
    {
        StartNotification.SetActive(true);
        StartCoroutine(HideNotificationRoutine());
    }

    private IEnumerator hidePrompts;
    private bool needRepeat;
    private void HidePrompts()
    {
        if (hidePrompts == null || needRepeat)
        {
            needRepeat = false;
            hidePrompts = HidePromptsRoutine();
            StartCoroutine(hidePrompts);
        }
    }

    private void ShowPrompts()
    {
        needRepeat = true;
        print("SHOOOW");
        StopCoroutine(hidePrompts);

        var aimColor = AimArenaOutline.color;
        aimColor.a = 0.4f; //~100 (rather 102)
        AimArenaOutline.color = aimColor;

        var heroColor = HeroArenaOutline.color;
        heroColor.a = 0.4f;
        HeroArenaOutline.color = heroColor;

        var shootColor = ShootArenaOutline.color;
        shootColor.a = 0.4f;
        ShootArenaOutline.color = shootColor;
    }

    private void OnDisable()
    {
        GameCore.HealthChangedEvent -= UpdateHealth;
        GameCore.AmmoChangedEvent -= UpdateAmmo;
        GameCore.GameDoStartedEvent -= ShowNotification;
        GameCore.GameDoCountdownEvent -= HidePrompts;
        GameCore.GameDoWaitedEvent -= ShowPrompts;

    }
    #endregion
    #region Private methods
    private IEnumerator HideNotificationRoutine()
    {
        yield return new WaitForSeconds(NOTIFICATION_HIDING_DELAY);

        var image = StartNotification.GetComponent<Image>();
        var newColor = image.color;
        for (float alpha = START_ALPHA; alpha > END_ALPHA; alpha -= NOTIFICATION_HIDING_PER_FRAME)
        {
            newColor.a = alpha;
            image.color = newColor;
            yield return null;
        }

        StartNotification.SetActive(false);
    }


    private IEnumerator HidePromptsRoutine()
    {
        var step = 0.01f;
        var aimColor = AimArenaOutline.color;
        var heroColor = HeroArenaOutline.color;
        var shootColor = ShootArenaOutline.color;

        while (aimColor.a > 0 || HeroArenaOutline.color.a > 0 || ShootArenaOutline.color.a > 0)
        {
            aimColor.a -= step;
            AimArenaOutline.color = aimColor;

            heroColor.a -= step;
            HeroArenaOutline.color = heroColor;

            shootColor.a -= step;
            ShootArenaOutline.color = shootColor;

            yield return null;
        }
    }


    private void UpdateAmmo(HeroType hero, int currentAmmo)
    {
        var text = currentAmmo + "/6";
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
        var fillAmount = currentHealth * ONE_PERCENT;
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