using System;

[Serializable]
public class DialogueFinishedEvent : IEvent
{
    public readonly Dialogue Dialogue;

    public string A;
    public DialogueFinishedEvent(Dialogue dialogue)
    {
        Dialogue = dialogue;
    }
}
