using UnityEngine;

public class HeroChooser : MonoBehaviour
{
    [SerializeField] private Transform heroGroup;

    private HeroContainer currentSelectedHero;
    private GameObject currentHero;

    private void Awake()
    {
        var currentOwnHeroTypeId = PlayerPrefs.GetInt("CurrentOwnHero");
        SelectHero(currentOwnHeroTypeId);
    }


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


    private void InstantiateHero(int currentHeroTypeId)
    {
        var currentHeroType = (HeroType)currentHeroTypeId;
        var hero = InstantiateHero(currentHeroType);

        UpdateHero(hero);

        StartTavernAnimation(currentHeroType);
    }


    private GameObject InstantiateHero(HeroType currentHeroType)
    {
        var heroPrefab = Resources.Load<GameObject>("Prefabs/Heroes/" + currentHeroType);

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
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Prefabs/Tavern");
        animator.SetInteger("HeroTypeId", (int)currentHeroType);
    }
}
