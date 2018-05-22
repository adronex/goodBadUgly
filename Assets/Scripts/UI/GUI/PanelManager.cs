using UnityEngine;


namespace UI.GUI
{
    public class PanelManager : MonoBehaviour
    {
        #region Fields
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject settings;
        [SerializeField] private GameObject stats;
        [SerializeField] private GameObject heroChooser;
        #endregion
        #region Settings
        public void ShowSettings()
        {
            settings.SetActive(true);
        }


        public void HideSettings()
        {
            settings.SetActive(false);
        }
        #endregion
        #region Stats
        public void ShowStats()
        {
            stats.SetActive(true);
        }


        public void HideStats()
        {
            stats.SetActive(false);
        }
        #endregion
        #region HeroChooser
        public void ShowHeroChooser()
        {
            heroChooser.SetActive(true);
        }


        public void HideHeroChooser()
        {
            heroChooser.SetActive(false);
        }
        #endregion
    }
}