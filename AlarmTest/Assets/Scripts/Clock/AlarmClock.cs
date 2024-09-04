using Helpers.Observer;
using System;
using UnityEngine;

namespace Behaviours
{
    sealed class AlarmClock : IClock, IEventListener<AlarmEvent>, IEventListener<TimeEvent>
    {
        private TimeSpan _alarmTime;

        private bool _isAlarmSet;

        public AlarmClock()
        {
            this.EventStartListening<AlarmEvent>();
            this.EventStartListening<TimeEvent>();
        }
        ~AlarmClock()
        {
            this.EventStopListening<AlarmEvent>();
            this.EventStopListening<TimeEvent>();
        }

        public void StartTime()
        {
            _isAlarmSet = true;
        }
        public void StopTime()
        {
            _isAlarmSet = false;
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
                var substruct = _alarmTime.Subtract(eventType.TimeValue);
                if (substruct.Hours == -12|| substruct.Hours == 12 && substruct.Minutes == 0)
                {
                    AlarmEvent.Trigger(AlarmEventType.ActivateAlarm);
                    StopTime();
                }
            }
        }
    }
}
