﻿using UI;
using UnityEngine;

namespace Animations
{
    public class FalseStartAnimation : MonoBehaviour
    {
        #region Fields
        private Transform gunpoint;
        private Transform rightLeg;
        private Transform leftLeg;
        private Transform hero;
        private Transform body;
        private Transform falseStart;

        private Transform gunParent;
        private Quaternion gunRotation;
        private Vector3 gunPosition;
        #endregion
        #region Unity lifecycle
        private void Start()
        {
            var heroInfo = GetComponent<HeroInfo>();
            gunpoint = heroInfo.Gunpoint;
            hero = heroInfo.Hero;
            rightLeg = heroInfo.RightLeg;
            leftLeg = heroInfo.LeftLeg;
            body = heroInfo.Body;
            falseStart = heroInfo.FalseStart;

            var transform = gunpoint.transform;
            gunRotation = transform.localRotation;
            gunPosition = transform.localPosition;
        }
        #endregion
        #region Public methods
        public void FalseStartStarting()
        {
            gunParent = gunpoint.parent;
            gunpoint.SetParent(falseStart);
            rightLeg.SetParent(hero);
            leftLeg.SetParent(hero);
        }


        public void TakeUpGun()
        {
            gunpoint.SetParent(gunParent);
            gunpoint.localRotation = gunRotation;
            gunpoint.localPosition = gunPosition;
        }


        public void FalseStartFinished()
        {
            gunParent = null;
            rightLeg.SetParent(body);
            leftLeg.SetParent(body);
        }
        #endregion
    }
}
