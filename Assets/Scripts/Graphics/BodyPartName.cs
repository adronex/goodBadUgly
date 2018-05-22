using UnityEngine;

namespace Graphics
{
    class BodyPartName : MonoBehaviour
    {
        #region Fields
        [SerializeField] BodyPart name;
        #endregion
        #region Properties
        public BodyPart Name
        {
            get { return name; }
        }
        #endregion
        public enum BodyPart { Head, Body, Leg }
    }
}
