using Core;
using UnityEngine;

namespace UI.GUI
{
    public class HeroChooser : MonoBehaviour
    {
        #region Fields
        [SerializeField] private Transform heroGroup;

        private HeroContainer currentSelectedHero;
        private GameObject currentHero;
        #endregion
        #region Unity lifecycle
        private void Awake()
        {
            var currentOwnHeroTypeId = PlayerPrefs.GetInt(Helps.CurrentOwnHeroData);
            SelectHero(currentOwnHeroTypeId);
        }
        #endregion
        #region Public methods
        public void SelectHero(int id)
        {
            InstantiateHero(id);

            if (currentSelectedHero != null)
            {
                currentSelectedHero.Deselect();
            }

            var child = heroGroup.GetChild(id);
            currentSelectedHero = child.GetComponent<HeroContainer>();

            currentSelectedHero.Select();
        }
        #endregion
        #region Private methods
        private void InstantiateHero(int currentHeroTypeId)
        {
            var currentHeroType = (HeroType)currentHeroTypeId;
            var hero = InstantiateHero(currentHeroType);

            UpdateHero(hero);

            StartTavernAnimation(currentHeroType);
        }


        private GameObject InstantiateHero(HeroType currentHeroType)
        {
            var heroPrefab = Resources.Load<GameObject>(Helps.HeroesPath + currentHeroType);

            return Instantiate(heroPrefab);
        }


        private void UpdateHero(GameObject hero)
        {
            if (currentHero != null)
            {
                Destroy(currentHero);
            }

            currentHero = hero;
        }


        private void StartTavernAnimation(HeroType currentHeroType)
        {
            var animator = currentHero.GetComponent<Animator>();
            animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(Helps.TavernAnimatorPath);
            animator.SetInteger(Helps.HeroTypeIdTavernAnim, (int)currentHeroType);
        }
        #endregion
    }
}