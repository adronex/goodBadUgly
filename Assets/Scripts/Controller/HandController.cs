using UnityEngine;

namespace Controller
{
    public class HandController
    {
        private readonly Transform transform;
        private readonly Transform gunpoint;

        public Transform GunPoint { get { return gunpoint; } }

        public HandController(Transform handAxis)
        {
            transform = handAxis;
            gunpoint = transform.Find("ArmLower").Find("Hand").Find("Gun").Find("Gunpoint"); 
        }

        public void LookTo(Vector2 aim) //INCORRECT METHOD! todo: 
        {
            var target = (Vector3)aim - gunpoint.position;
            var temp = Quaternion.LookRotation(target);
            transform.rotation = temp;
            var local = transform.localRotation.eulerAngles;
            var newLocal = new Vector3(0, 0, local.x);
            transform.localRotation = Quaternion.Euler(newLocal);
        }
    }
}