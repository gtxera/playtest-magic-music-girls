using UnityEngine;

public class InteractionInRangeEvent : IEvent
{
    public Interactable Interactable { get; }

    public InteractionInRangeEvent(Interactable interactable)
    {
        Interactable = interactable;
    }
}
