using System;

namespace Helpers.Observer
{
    class EventListenerWrapper<TOwner, TTarget, TEvent> : IEventListener<TEvent>, IDisposable
		where TEvent : struct
	{
		private Action<TTarget> _callback;

		private TOwner _owner;
		public EventListenerWrapper(TOwner owner, Action<TTarget> callback)
		{
			_owner = owner;
			_callback = callback;
			RegisterCallbacks(true);
		}

		public void OnEventTrigger(TEvent eventType)
		{
			var item = OnEvent(eventType);
			_callback?.Invoke(item);
		}

		protected virtual TTarget OnEvent(TEvent eventType) => default;

		private void RegisterCallbacks(bool isAlive)
		{
			if (isAlive)
			{
				this.EventStartListening<TEvent>();
			}
			else
			{
				this.EventStopListening<TEvent>();
			}
		}

		public void Dispose()
		{
			RegisterCallbacks(false);
			_callback = null;
		}
	}
}
