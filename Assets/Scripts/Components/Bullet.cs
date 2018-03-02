using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Fields
    public const float BULLET_SPEED = 1F;
    #endregion
    #region Unity lifecycle
    private void Update()
    {
        transform.position += transform.right * BULLET_SPEED;
    }
    #endregion
}
