using UnityEngine;
using TMPro;
using Helpers.Extensions;
using Helpers.Observer;
using System;

namespace GameUI
{
    sealed class TimeUI : MonoBehaviour, IEventListener<TimeEvent>
    {
        [SerializeField] private Transform _hoursHand;
        [SerializeField] private Transform _minutesHand;
        [SerializeField] private Transform _secondsHand;
        [SerializeField] private TextMeshProUGUI _timerText;

        private void OnEnable()
        {
            this.EventStartListening<TimeEvent>();

        }
        private void OnDisable()
        {
            this.EventStopListening<TimeEvent>();

        }
        private void FillText(TimeSpan timerValue)
        {
            var valueConverted = DateTimeExtension.FromDoubleToString(timerValue.TotalSeconds);
            _timerText.text = valueConverted;
        }
        private void SetClockHands(TimeSpan timerValue)
        {
            _hoursHand.localRotation = Quaternion.Euler(0f, 0f, (float)timerValue.TotalHours * -DateTimeExtension.DEGREES_IN_HOUR);
            _minutesHand.localRotation = Quaternion.Euler(0f, 0f, (float)timerValue.TotalMinutes * -DateTimeExtension.DEGREES_IN_MINUTE);
            _secondsHand.localRotation = Quaternion.Euler(0f, 0f, (float)timerValue.TotalSeconds * -DateTimeExtension.DEGREES_IN_SECOND);
        }
        private void FillText(string rewardText)
        {
            _timerText.text = rewardText;
        }

        public void OnEventTrigger(TimeEvent eventType)
        {
            FillText(eventType.TimeValue);
            SetClockHands(eventType.TimeValue);
        }
    }
}
