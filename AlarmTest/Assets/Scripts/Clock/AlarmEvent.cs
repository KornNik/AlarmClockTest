using System;

namespace Helpers.Observer
{
    enum AlarmEventType
    {
        None,
        SetAlarm,
        ActivateAlarm,
        ButtonDown
    }
    struct AlarmEvent
    {
        private static AlarmEvent _alarmEvent;

        private AlarmEventType _eventType;
        private TimeSpan _alarmTime;

        public AlarmEventType EventType => _eventType;
        public TimeSpan AlarmTime => _alarmTime;

        public static void Trigger(AlarmEventType eventType, TimeSpan alarmTime = default)
        {
            _alarmEvent._eventType = eventType;
            _alarmEvent._alarmTime = alarmTime;
            EventManager.TriggerEvent(_alarmEvent);
        }
    }
}
