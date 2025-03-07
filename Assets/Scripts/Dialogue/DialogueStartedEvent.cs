using UnityEngine;

public class DialogueStartedEvent : IEvent
{
    public readonly Dialogue Dialogue;

    public DialogueStartedEvent(Dialogue dialogue)
    {
        Dialogue = dialogue;
    }
}
