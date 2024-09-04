namespace Helpers.Observer
{
    interface IEventListener<T> : IEventListenerBase
    {
        void OnEventTrigger(T eventType);
    }
}
