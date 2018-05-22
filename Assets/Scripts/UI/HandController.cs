using UnityEngine;

namespace UI
{
    public class HandController
    {
        #region Fields
        private readonly Transform armUpper;
        private readonly Transform armLower;
        private readonly Transform hand;
        private readonly Transform gun;
        #endregion
        #region Properties
        public Transform Gunpoint { get { return gun; } }
        #endregion
        #region Public methods
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
            var deltaX = (armUpper.position - gun.position).y - 0.9f;

            var offsetPosition = new Vector3(0f, -deltaX, 0f);

            armUpper.up = -aim - offsetPosition * 2 + armUpper.position;
            armLower.up = -aim * 2 - offsetPosition + armLower.position + hand.position;

            var euler = armUpper.localEulerAngles;
            armUpper.localEulerAngles = new Vector3(0, 0, euler.z);

            euler = armLower.localEulerAngles;
            armLower.localEulerAngles = new Vector3(0, 0, euler.z);
        }
        #endregion
    }
}