using UnityEngine;

public class Element : MonoBehaviour
{
    private static App app;

    public App App { get { return app; } }

    private void Awake()
    {
        app = FindObjectOfType<App>();
    }
}
