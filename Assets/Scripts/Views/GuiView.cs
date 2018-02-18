using UnityEngine.UI;

public class GuiView
{
    private Text OwnAmmo;
    private Image EnemyHealth;

    public GuiView(Text ownAmmo, Image enemyHealth)
    {
        OwnAmmo = ownAmmo;
        EnemyHealth = enemyHealth;
    }

    public void UpdateAmmo(Hero hero, int currentAmmo)
    {
        switch (hero)
        {
            case Hero.Own:
                OwnAmmo.text = currentAmmo + "/6";
                break;
            case Hero.Enemy:
                break;
        }
    }


    public void UpdateHealth(Hero hero, int currentHealth)
    {
        switch (hero)
        {
            case Hero.Own:
                break;
            case Hero.Enemy:
                EnemyHealth.fillAmount = 0.01f * currentHealth;
                break;
        }
    }


    public void HeroDead(Hero hero)
    {
        App.View.Print(hero + "is dead");
    }
}