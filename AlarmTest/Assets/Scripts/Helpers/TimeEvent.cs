using System;

namespace Helpers.Observer
{
    struct TimeEvent
    {
        private static TimeEvent _timerEvent;
        private TimeSpan _timeValue;

        public TimeSpan TimeValue => _timeValue;

        public static void Trigger(TimeSpan timerValue)
        {
            _timerEvent._timeValue = timerValue;
            EventManager.TriggerEvent(_timerEvent);
        }
    }
}
