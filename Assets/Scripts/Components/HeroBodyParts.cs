using UnityEngine;

public class HeroBodyParts : MonoBehaviour
{
    [SerializeField] private Transform[] bodyParts;
    [SerializeField] private GameObject bullet;

    public int Lenght { get { return bodyParts.Length; } }

    public Transform Get(int i)
    {
        return bodyParts[i];
    }
}
