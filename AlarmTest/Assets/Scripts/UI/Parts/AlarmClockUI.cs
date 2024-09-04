using Helpers.Extensions;
using Helpers.Observer;
using System;
using TMPro;
using UnityEngine;

namespace GameUI
{
    sealed class AlarmClockUI : MonoBehaviour, IEventListener<AlarmTimeEvent>, IEventListener<AlarmEvent>
    {
        [SerializeField] private Transform _hoursHand;
        [SerializeField] private Transform _minutesHand;
        [SerializeField] private Transform _secondsHand;
        [SerializeField] private TMP_InputField _timerText;

        private void Awake()
        {

        }
        private void OnEnable()
        {
            this.EventStartListening<AlarmTimeEvent>();
            this.EventStartListening<AlarmEvent>();
        }
        private void OnDisable()
        {
            this.EventStopListening<AlarmTimeEvent>();
            this.EventStopListening<AlarmEvent>();
        }

        private void SyncAlarmWithClockTime(TimeSpan time)
        {
            SetClockHands(time);
            FillText(time);
        }
        private void SetClockHands(TimeSpan timerValue)
        {
            _hoursHand.localRotation = Quaternion.Euler(0f, 0f, 
                (float)timerValue.TotalHours * -DateTimeExtension.DEGREES_IN_HOUR);
            _minutesHand.localRotation = Quaternion.Euler(0f, 0f, 
                (float)timerValue.TotalMinutes * -DateTimeExtension.DEGREES_IN_MINUTE);
            _secondsHand.localRotation = Quaternion.Euler(0f, 0f, 
                (float)timerValue.TotalSeconds * -DateTimeExtension.DEGREES_IN_SECOND);
        }
        private void FillText(TimeSpan timerValue)
        {
            var valueConverted = DateTimeExtension.FromDoubleToString(timerValue.TotalSeconds);
            _timerText.text = valueConverted;
        }
        private void SetAlarm()
        {
            AlarmEvent.Trigger(AlarmEventType.SetAlarm, HandsDegreesInTime());
        }
        private TimeSpan HandsDegreesInTime()
        {
            TimeSpan alarmTime;
            int alarmHour = (int)Mathf.Abs((_hoursHand.localRotation.eulerAngles.z - 360) / DateTimeExtension.DEGREES_IN_HOUR);
            int alarmMinute = (int)Mathf.Abs((_minutesHand.localRotation.eulerAngles.z - 360) / DateTimeExtension.DEGREES_IN_MINUTE);
            int alarmSeconds = (int)Mathf.Abs((_secondsHand.localRotation.eulerAngles.z - 360) / DateTimeExtension.DEGREES_IN_SECOND);
            alarmTime = new TimeSpan(alarmHour, alarmMinute, alarmSeconds);
            Debug.Log($"alarmTime{alarmHour}||{alarmMinute}||{alarmSeconds}");
            return alarmTime;
        }

        public void OnEventTrigger(AlarmTimeEvent eventType)
        {
            SyncAlarmWithClockTime(eventType.TimeValue);
        }

        public void OnEventTrigger(AlarmEvent eventType)
        {
            if (eventType.EventType == AlarmEventType.ButtonDown)
            {
                SetAlarm();
            }
        }
    }
}
