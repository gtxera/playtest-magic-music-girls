using System.Collections.Generic;

public class EventBus
{
    private readonly List<IEventListener> _listeners = new();
    
    public void Publish<TEvent>(TEvent @event) where TEvent : class, IEvent
    {
        foreach (var listener in _listeners)
        {
            if (listener is IEventListener<TEvent> typedListener)
            {
                typedListener.Handle(@event);
            }
        }
    }

    public void Subscribe(IEventListener listener) => _listeners.Add(listener);

    public void Unsubscribe(IEventListener listener) => _listeners.Remove(listener);
}
