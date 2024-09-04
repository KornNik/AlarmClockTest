namespace Helpers.Observer
{
    struct GameEvent
    {
		public string EventName;
		public GameEvent(string newName)
		{
			EventName = newName;
		}
		static GameEvent gameEvent;
		public static void Trigger(string newName)
		{
			gameEvent.EventName = newName;
			EventManager.TriggerEvent(gameEvent);
		}
	}
}
