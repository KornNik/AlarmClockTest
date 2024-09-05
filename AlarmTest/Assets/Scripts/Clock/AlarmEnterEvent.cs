namespace Helpers.Observer
{
    enum AlarmEnterType
    {
        None,
        Hands,
        Digits
    }
    struct AlarmEnterEvent
    {
        private static AlarmEnterEvent _alarmEvent;

        private AlarmEnterType _eventType;

        public AlarmEnterType EventType => _eventType;

        public static void Trigger(AlarmEnterType eventType)
        {
            _alarmEvent._eventType = eventType;
            EventManager.TriggerEvent(_alarmEvent);
        }
    }
}
