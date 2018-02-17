using UnityEngine;
using UnityEngine.UI;

public class View : MonoBehaviour
{
    [SerializeField] private Text ownAmmo;
    [SerializeField] private Image enemyHealth;

    public GuiView Gui;


    private void Awake()
    {
        Gui = new GuiView(ownAmmo, enemyHealth);
    }


    private void Update()
    {
        App.Controller.Area.FindClickedAreas(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.W) && App.Model.OwnHero.CanShoot)
        {
            App.Controller.Shoot();
        }
    }


    public void Print(string text)
    {
        print(text);
    }
}
