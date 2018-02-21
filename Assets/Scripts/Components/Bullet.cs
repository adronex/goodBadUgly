using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Fields
    private const float speed = 15f;
    #endregion
    #region Properties
    public static float Speed
    {
        get { return speed * Time.deltaTime; }
    }
    #endregion
    #region Unity lifecycle
    private void Update()
    {
        transform.position += transform.right * Speed;
    }
    #endregion
}
