using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using Core;

namespace UI.GUI
{
    public class GuiManager : MonoBehaviour
    {
        #region Fields
        private const float NOTIFICATION_HIDING_PER_FRAME = 0.02f;
        private const float NOTIFICATION_HIDING_DELAY = 0.3f;
        private const float START_ALPHA = 1f;
        private const float END_ALPHA = 0f;

        private const float ONE_PERCENT = 0.01F;

        [Header("Notification")]
        [SerializeField]
        private GameObject StartNotification;
        [Header("Areas")]
        [SerializeField]
        private Image AimAreasOutline;
        [SerializeField] private Image HeroAreasOutline;
        [SerializeField] private Image ShootAreasOutline;
        [SerializeField] private GameObject GameOverPanel;
        [SerializeField] private GameObject DrawPanel;
        [Header("Own hero")]
        [SerializeField]
        private Text ownAmmoText;
        [SerializeField] private Image ownHpImage;
        [Header("Enemy hero")]
        [SerializeField]
        private Text enemyAmmoText;
        [SerializeField] private Image enemyHpImage;

        private IEnumerator hidePrompts;
        private bool shouldShow;
        #endregion
        #region Unity lifecycle
        private void OnEnable()
        {
            GameCore.GameStartingEvent += ShowNotification;
            GameCore.StartingCountdownEvent += HidePrompts;
            GameCore.GameWaitingEvent += ShowPrompts;
            GameCore.GameEndingEvent += ShowGameOverPanel;
            Hero.HpChangedEvent += Hero_HpChangedEvent;
            Hero.AmmoChangedEvent += Hero_AmmoChangedEvent;

            GameCore.GameDrawEvent += ShowDrawPanel;
        }


        private void OnDisable()
        {
            GameCore.GameStartingEvent -= ShowNotification;
            GameCore.StartingCountdownEvent -= HidePrompts;
            GameCore.GameWaitingEvent -= ShowPrompts;
            GameCore.GameEndingEvent -= ShowGameOverPanel;
            Hero.HpChangedEvent -= Hero_HpChangedEvent;
            Hero.AmmoChangedEvent -= Hero_AmmoChangedEvent;

            GameCore.GameDrawEvent -= ShowDrawPanel;
        }
        #endregion

        #region Private methods
        private void Hero_AmmoChangedEvent(Hero hero, int currentAmmo, int maxAmmo)
        {
            var text = currentAmmo + "/" + maxAmmo;

            if (hero is OwnHero)
            {
                ownAmmoText.text = text;
            }
            else if (hero is EnemyHero)
            {
                enemyAmmoText.text = text;
            }
        }


        private void Hero_HpChangedEvent(Hero hero, int currentHp, int maxHp)
        {
            var fillAmount = currentHp * ONE_PERCENT;

            if (hero is OwnHero)
            {
                ownHpImage.fillAmount = fillAmount;
            }
            else if (hero is EnemyHero)
            {
                enemyHpImage.fillAmount = fillAmount;
            }
        }


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
            var aimColor = AimAreasOutline.color;
            var heroColor = HeroAreasOutline.color;
            var shootColor = ShootAreasOutline.color;

            while (aimColor.a > 0 || heroColor.a > 0 || shootColor.a > 0)
            {
                aimColor.a -= step;
                AimAreasOutline.color = aimColor;

                heroColor.a -= step;
                HeroAreasOutline.color = heroColor;

                shootColor.a -= step;
                ShootAreasOutline.color = shootColor;

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

            var aimColor = AimAreasOutline.color;
            aimColor.a = 0.4f; //~100 (more precisely 102)  
            AimAreasOutline.color = aimColor;

            var heroColor = HeroAreasOutline.color;
            heroColor.a = 0.4f;
            HeroAreasOutline.color = heroColor;

            var shootColor = ShootAreasOutline.color;
            shootColor.a = 0.4f;
            ShootAreasOutline.color = shootColor;
        }


        private void ShowGameOverPanel()
        {
            GameOverPanel.SetActive(true);
        }

        private void ShowDrawPanel()
        {
            DrawPanel.SetActive(true);
        }
        #endregion
    }
}