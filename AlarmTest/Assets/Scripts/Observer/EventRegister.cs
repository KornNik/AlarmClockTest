namespace Helpers.Observer
{
    static class EventRegister
    {
		public delegate void Delegate<T>(T eventType);

		public static void EventStartListening<EventType>(this IEventListener<EventType> caller) where EventType : struct
		{
			EventManager.AddListener(caller);
		}

		public static void EventStopListening<EventType>(this IEventListener<EventType> caller) where EventType : struct
		{
			EventManager.RemoveListener(caller);
		}
	}
}
