using Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GUI
{
    class HeroContainer : MonoBehaviour
    {
        [SerializeField] private int heroTypeId;
        [SerializeField] private Text pickText;


        internal void Select()
        {
            PlayerPrefs.SetInt("CurrentOwnHero", heroTypeId);

            pickText.text = "PICKED";

            var heroType = (HeroType)heroTypeId;
            OwnHero.heroType = heroType;
        }


        internal void Deselect()
        {
            pickText.text = "PICK";
        }
    }
}