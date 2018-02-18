using UnityEngine;

public class App : MonoBehaviour
{
    public static Model Model;
    public static View View;
    public static Controller Controller;

    public static Controller[] Controllers;

    public static GameObject ownHand;

    public static void Notify(Notification eventName)
    {
        foreach (var controller in Controllers)
        {
            controller.OnNotification(eventName);
        }
    }

    private void Awake()
    {
        Model = GetComponent<Model>();
        View = GetComponent<View>();
        Controller = GetComponent<Controller>();

        Controllers = FindObjectsOfType<Controller>();
        ownHand = GameObject.Find("OwnHero");
    }

    private void Start()
    {
        //init things here
    }
}
