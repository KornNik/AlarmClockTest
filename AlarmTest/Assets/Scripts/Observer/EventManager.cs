using System;
using System.Collections.Generic;

namespace Helpers.Observer
{
	static class EventManager
	{
		private static Dictionary<Type, List<IEventListenerBase>> _subscribersList;

		static EventManager()
		{
			_subscribersList = new Dictionary<Type, List<IEventListenerBase>>();
		}

		/// <summary>
		/// Adds a new subscriber to a certain event.
		/// </summary>
		/// <param name="listener">listener.</param>
		/// <typeparam name="Event">The event type.</typeparam>
		public static void AddListener<Event>(IEventListener<Event> listener) where Event : struct
		{
			Type eventType = typeof(Event);

			if (!_subscribersList.ContainsKey(eventType))
			{
				_subscribersList[eventType] = new List<IEventListenerBase>();
			}

			if (!SubscriptionExists(eventType, listener))
			{
				_subscribersList[eventType].Add(listener);
			}
		}

		/// <summary>
		/// Removes a subscriber from a certain event.
		/// </summary>
		/// <param name="listener">listener.</param>
		/// <typeparam name="Event">The event type.</typeparam>
		public static void RemoveListener<Event>(IEventListener<Event> listener) where Event : struct
		{
			Type eventType = typeof(Event);

			if (!_subscribersList.ContainsKey(eventType)) { return; }

			List<IEventListenerBase> subscriberList = _subscribersList[eventType];


			for (int i = subscriberList.Count - 1; i >= 0; i--)
			{
				if (subscriberList[i] == listener)
				{
					subscriberList.Remove(subscriberList[i]);

					if (subscriberList.Count == 0)
					{
						_subscribersList.Remove(eventType);
					}

					return;
				}
			}
		}

		/// <summary>
		/// Triggers an event. All instances that are subscribed to it will receive it (and will potentially act on it).
		/// </summary>
		/// <param name="newEvent">The event to trigger.</param>
		/// <typeparam name="MMEvent">The 1st type parameter.</typeparam>
		public static void TriggerEvent<Event>(Event newEvent) where Event : struct
		{
			List<IEventListenerBase> list;
			if (!_subscribersList.TryGetValue(typeof(Event), out list)) { return; }

			for (int i = list.Count - 1; i >= 0; i--)
			{
				(list[i] as IEventListener<Event>).OnEventTrigger(newEvent);
			}
		}

		/// <summary>
		/// Checks if there are subscribers for a certain type of events
		/// </summary>
		/// <returns><c>true</c>, if exists was subscriptioned, <c>false</c> otherwise.</returns>
		/// <param name="type">Type.</param>
		/// <param name="receiver">Receiver.</param>
		private static bool SubscriptionExists(Type type, IEventListenerBase receiver)
		{
			List<IEventListenerBase> receivers;

			if (!_subscribersList.TryGetValue(type, out receivers)) { return false; }

			bool exists = false;

			for (int i = receivers.Count - 1; i >= 0; i--)
			{
				if (receivers[i] == receiver)
				{
					exists = true;
					break;
				}
			}

			return exists;
		}
	}
}
