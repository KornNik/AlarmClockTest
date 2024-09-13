using Helpers.Extensions;
using Helpers.Observer;
using System;

namespace Behaviours
{
    sealed class AlarmClock : IEventSubscription, IEventListener<AlarmEvent>, IEventListener<TimeEvent>, IEventListener<AlarmEnterEvent>
    {
        private TimeSpan _alarmTime;
        private AlarmEnterType _alarmEnterType = default;

        private bool _isAlarmSet;

        public AlarmClock()
        {
        }

        public void StartTime()
        {
            _isAlarmSet = true;
        }
        public void StopTime()
        {
            _isAlarmSet = false;
        }

        public void Subscribe()
        {
            this.EventStartListening<AlarmEvent>();
            this.EventStartListening<TimeEvent>();
            this.EventStartListening<AlarmEnterEvent>();
        }

        public void Unsubscribe()
        {
            this.EventStopListening<AlarmEvent>();
            this.EventStopListening<TimeEvent>();
            this.EventStopListening<AlarmEnterEvent>();
        }

        public void OnEventTrigger(AlarmEvent eventType)
        {
            if (eventType.EventType == AlarmEventType.SetAlarm)
            {
                _alarmTime = eventType.AlarmTime;
                StartTime();
            }
        }

        public void OnEventTrigger(TimeEvent eventType)
        {
            if (_isAlarmSet)
            {
                if (_alarmEnterType == AlarmEnterType.Hands)
                {
                    if (DateTimeExtension.IsTimeEqualHands(_alarmTime, eventType.TimeValue))
                    {
                        AlarmEvent.Trigger(AlarmEventType.ActivateAlarm);
                        StopTime();
                    }
                }
                else if (_alarmEnterType == AlarmEnterType.Digits)
                {
                    if (DateTimeExtension.IsTimeEqualDigit(_alarmTime, eventType.TimeValue))
                    {
                        AlarmEvent.Trigger(AlarmEventType.ActivateAlarm);
                        StopTime();
                    }
                }
            }
        }

        public void OnEventTrigger(AlarmEnterEvent eventType)
        {
            _alarmEnterType = eventType.EventType;
        }
    }
}
