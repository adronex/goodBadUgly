using Core;
using Core.Heroes;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GUI
{
    class HeroContainer : MonoBehaviour
    {
        #region Fields
        [SerializeField] private int heroTypeId;
        [SerializeField] private Text pickText;
        #endregion
        #region Public methods
        internal void Select()
        {
            PlayerPrefs.SetInt(Helps.CurrentOwnHeroData, heroTypeId);

            pickText.text = Helps.HeroSelected;

            var heroType = (HeroType)heroTypeId;
            OwnHero.heroType = heroType;
        }


        internal void Deselect()
        {
            pickText.text = Helps.HeroUnselected;
        }
        #endregion
    }
}