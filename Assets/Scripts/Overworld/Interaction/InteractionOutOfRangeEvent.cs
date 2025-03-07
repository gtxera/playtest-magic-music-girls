using UnityEngine;

public class InteractionOutOfRangeEvent : IEvent
{
    public Interactable Interactable { get; }

    public InteractionOutOfRangeEvent(Interactable interactable)
    {
        Interactable = interactable;
    }
}
