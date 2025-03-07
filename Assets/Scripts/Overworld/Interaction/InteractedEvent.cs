using UnityEngine;

public class InteractedEvent : IEvent
{
    public Interactable Interactable { get; }

    public InteractedEvent(Interactable interactable)
    {
        Interactable = interactable;
    }
}
