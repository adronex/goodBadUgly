using System;
using UnityEngine;
using UnityEngine.UI;

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
