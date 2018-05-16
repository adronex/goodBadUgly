using UI;
using UnityEngine;

namespace Controller
{
    public class HandController
    {
        private readonly Transform armUpper;
        private readonly Transform armLower;
        private readonly Transform hand;
        private readonly Transform gun;

        public Transform Gunpoint { get { return gun; } }

        public HandController(HeroInfo heroInfo)
        {
            gun = heroInfo.Gunpoint;
            hand = gun.parent;
            armLower = hand.parent;
            armUpper = armLower.parent;
        }

        public void LookTo(Vector3 aim)
        {
            //I DON'T KNOW HOW IT WORKS BUT IM VERY HAPPY
            var deltaX = (gun.position - armUpper.position).x;
            var offset = new Vector3(0f, -deltaX, 0f);

            armUpper.up = -aim - offset * 2 + armUpper.position;
            armLower.up = -aim * 2 - offset + armLower.position + hand.position;

            var euler = armUpper.localEulerAngles;
            armUpper.localEulerAngles = new Vector3(0, 0, euler.z);

            euler = armLower.localEulerAngles;
            armLower.localEulerAngles = new Vector3(0, 0, euler.z);
        }
    }
}