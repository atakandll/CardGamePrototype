﻿using UICardHand;
using UnityEngine;

namespace UICardTransform
{
    public class UIMotionRotationCard : UIMotionBaseCard
    {
        public UIMotionRotationCard(IUICard handler) : base(handler)
        {
        }

        protected override float Threshold => 0.05f;

        protected override void OnMotionEnds()
        {
            Handler.transform.eulerAngles = Target;
            IsOperating = false;
            OnFinishMotion?.Invoke();
        }
        protected override void KeepMotion()
        {
            var current = Handler.transform.rotation;
            var amount = Speed * Time.deltaTime;
            var rotation = Quaternion.Euler(Target);
            var newRotation = Quaternion.RotateTowards(current, rotation, amount);
            Handler.transform.rotation = newRotation;

        }

        protected override bool CheckFinalState()
        {
            var distance = Target - Handler.transform.eulerAngles;
            var smallerThanLimit = distance.magnitude <= Threshold;
            var equals360 = (int) distance.magnitude == 360;
            var isFinal = smallerThanLimit || equals360;
            return isFinal;
        }
    }
}