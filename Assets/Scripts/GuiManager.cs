using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System;

public class GuiManager : MonoBehaviour
{
    #region Fields
    private const float NOTIFICATION_HIDING_PER_FRAME = 0.02f;
    private const float NOTIFICATION_HIDING_DELAY = 0.3f;
    private const float START_ALPHA = 1f;
    private const float END_ALPHA = 0f;

    [SerializeField] private GameObject StartNotification;
    [SerializeField] private Image AimArenaOutline;
    [SerializeField] private Image HeroArenaOutline;
    [SerializeField] private Image ShootArenaOutline;
    [SerializeField] private GameObject GameOverPanel;

    private IEnumerator hidePrompts;
    private bool shouldShow;
    #endregion
    #region Unity lifecycle
    private void OnEnable()
    {
        GameCore.GameDoStartedEvent += ShowNotification;
        GameCore.GameDoCountdownEvent += HidePrompts;
        GameCore.GameDoWaitedEvent += ShowPrompts;
        GameCore.GameOverEvent += ShowGameOverPanel;
    }

   
    private void OnDisable()
    {
        GameCore.GameDoStartedEvent -= ShowNotification;
        GameCore.GameDoCountdownEvent -= HidePrompts;
        GameCore.GameDoWaitedEvent -= ShowPrompts;
        GameCore.GameOverEvent -= ShowGameOverPanel;
    }
    #endregion
    #region Private methods
    private void ShowNotification()
    {
        StartNotification.SetActive(true);
        StartCoroutine(HideNotificationRoutine());
    }


    private IEnumerator HideNotificationRoutine()
    {
        yield return new WaitForSeconds(NOTIFICATION_HIDING_DELAY);

        var image = StartNotification.GetComponent<Image>();
        var newColor = image.color;
        for (var alpha = START_ALPHA; alpha > END_ALPHA; alpha -= NOTIFICATION_HIDING_PER_FRAME)
        {
            newColor.a = alpha;
            image.color = newColor;
            yield return null;
        }

        StartNotification.SetActive(false);
    }


    private IEnumerator HidePromptsRoutine()
    {
        const float step = 0.01f;
        var aimColor = AimArenaOutline.color;
        var heroColor = HeroArenaOutline.color;
        var shootColor = ShootArenaOutline.color;

        while (aimColor.a > 0 || heroColor.a > 0 || shootColor.a > 0)
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


    private void HidePrompts()
    {
        if (hidePrompts != null && !shouldShow) return;

        shouldShow = false;
        hidePrompts = HidePromptsRoutine();

        StartCoroutine(hidePrompts);
    }


    private void ShowPrompts()
    {
        shouldShow = true;
        StopCoroutine(hidePrompts);

        var aimColor = AimArenaOutline.color;
        aimColor.a = 0.4f; //~100 (more precisely 102)  
        AimArenaOutline.color = aimColor;

        var heroColor = HeroArenaOutline.color;
        heroColor.a = 0.4f;
        HeroArenaOutline.color = heroColor;

        var shootColor = ShootArenaOutline.color;
        shootColor.a = 0.4f;
        ShootArenaOutline.color = shootColor;
    }


    private void ShowGameOverPanel()
    {
        GameOverPanel.SetActive(true);
    }
    #endregion
}
