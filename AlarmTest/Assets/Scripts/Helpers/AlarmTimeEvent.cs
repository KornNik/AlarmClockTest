using System;

namespace Helpers.Observer
{
    struct AlarmTimeEvent
    {
        private static AlarmTimeEvent _alarmTimeEvent;
        private TimeSpan _timeValue;

        public TimeSpan TimeValue => _timeValue;

        public static void Trigger(TimeSpan timerValue)
        {
            _alarmTimeEvent._timeValue = timerValue;
            EventManager.TriggerEvent(_alarmTimeEvent);
        }
    }
}
