using UnityEngine;

public class BodyPartInfo : MonoBehaviour
{
    [SerializeField] private int minDamage;
    [SerializeField] private int maxDamage;

    public int MinDamage { get { return minDamage; } }
    public int MaxDamage { get { return maxDamage; } }
}
