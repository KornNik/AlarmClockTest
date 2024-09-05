using Helpers.Extensions;
using Helpers.Observer;
using System;
using UnityEngine;
using TMPro;

namespace GameUI
{
    internal sealed class AlarmClockUI : ClockBase<TMP_InputField>, IEventListener<AlarmTimeEvent>,
        IEventListener<AlarmEvent>, IEventListener<AlarmEnterEvent>
    {
        private AlarmEnterType _alarmEnterType = default;

        private void Awake()
        {
        }

        private void OnEnable()
        {
            this.EventStartListening<AlarmTimeEvent>();
            this.EventStartListening<AlarmEvent>();
            this.EventStartListening<AlarmEnterEvent>();
        }

        private void OnDisable()
        {
            this.EventStopListening<AlarmTimeEvent>();
            this.EventStopListening<AlarmEvent>();
            this.EventStopListening<AlarmEnterEvent>();
        }

        protected override void FillText(TimeSpan timerValue)
        {
            var valueConverted = DateTimeExtension.FromDoubleToString(timerValue.TotalSeconds);
            _timerText.text = valueConverted;
        }

        private void SyncAlarmWithClockTime(TimeSpan time)
        {
            SetClockHands(time);
            FillText(time);
        }

        private void SetAlarm(AlarmEnterType alarmEnterType)
        {
            switch (alarmEnterType)
            {
                case AlarmEnterType.Hands:
                    AlarmEvent.Trigger(AlarmEventType.SetAlarm, DateTimeExtension.HandsDegreesInTime
                        (_hoursHand.localRotation.eulerAngles, _minutesHand.localRotation.eulerAngles,
                        _secondsHand.localRotation.eulerAngles));
                    break;
                case AlarmEnterType.Digits:
                    AlarmEvent.Trigger(AlarmEventType.SetAlarm, DateTimeExtension.EnterStringInTime(_timerText.text));
                    break;
                default:
                    break;

            }
        }

        public void OnEventTrigger(AlarmTimeEvent eventType)
        {
            SyncAlarmWithClockTime(eventType.TimeValue);
        }
        public void OnEventTrigger(AlarmEvent eventType)
        {
            if (eventType.EventType == AlarmEventType.ButtonDown)
            {
                SetAlarm(_alarmEnterType);
            }
        }
        public void OnEventTrigger(AlarmEnterEvent eventType)
        {
            _alarmEnterType = eventType.EventType;
        }
    }
}