using UnityEngine;

public class BodyPartInfo : MonoBehaviour
{
    #region Fields
    [SerializeField] private int damage;
    #endregion

    #region Properties
    public int Damage { get { return damage; } }
    #endregion
}
