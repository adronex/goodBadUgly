using UnityEngine;

namespace Controller
{
    public class HandController
    {
        private Transform transform;
        private Transform gunpoint;

        public Vector2 Position { get { return transform.position; } }
        public Transform GunPoint { get { return gunpoint; } }

        public HandController(Transform handAxis)
        {
            transform = handAxis;
            gunpoint = transform.Find("Hand").Find("Gunpoint");
        }

        public void LookTo(Vector2 aim)
        {
            transform.rotation = Quaternion.LookRotation(aim);
        }
    }
}