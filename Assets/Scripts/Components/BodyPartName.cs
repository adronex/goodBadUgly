using UnityEngine;

class BodyPartName : MonoBehaviour
{
    [SerializeField] BodyPart name;

    public BodyPart Name
    {
        get { return name; }
    }

    public enum BodyPart { Head, Body, Leg }
}
