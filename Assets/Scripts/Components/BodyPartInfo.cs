using UnityEngine;

public class BodyPartInfo : MonoBehaviour
{
    #region Fields
    [SerializeField] private int minDamage;
    [SerializeField] private int maxDamage;
    #endregion

    #region Properties
    public int MinDamage { get { return minDamage; } }
    public int MaxDamage { get { return maxDamage; } }
    #endregion
}
