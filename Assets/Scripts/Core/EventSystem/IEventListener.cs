using UnityEngine;

public interface IEventListener
{
}

public interface IEventListener<in TEvent> : IEventListener where TEvent : class, IEvent
{
    public void Handle(TEvent @event);
}
