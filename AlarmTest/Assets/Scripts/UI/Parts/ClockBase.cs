using System;
using UnityEngine;
using Helpers.Extensions;

namespace GameUI
{
    abstract class ClockBase<T> : MonoBehaviour
    {
        [SerializeField] protected Transform _hoursHand;
        [SerializeField] protected Transform _minutesHand;
        [SerializeField] protected Transform _secondsHand;
        [SerializeField] protected T _timerText;

        protected virtual void SetClockHands(TimeSpan timerValue)
        {
            _hoursHand.localRotation = Quaternion.Euler(0f, 0f, (float)timerValue.TotalHours * -DateTimeExtension.DEGREES_IN_HOUR);
            _minutesHand.localRotation = Quaternion.Euler(0f, 0f, (float)timerValue.TotalMinutes * -DateTimeExtension.DEGREES_IN_MINUTE);
            _secondsHand.localRotation = Quaternion.Euler(0f, 0f, (float)timerValue.TotalSeconds * -DateTimeExtension.DEGREES_IN_SECOND);
        }
        protected abstract void FillText(TimeSpan timerValue);
    }
}
