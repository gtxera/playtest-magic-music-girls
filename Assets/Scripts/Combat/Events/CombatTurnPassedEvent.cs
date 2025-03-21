using UnityEngine;

public class CombatTurnPassedEvent : IEvent
{
    public readonly int Turn;
    public readonly Unit Next;
    public readonly Unit Previous;

    public CombatTurnPassedEvent(Unit next, Unit previous, int turn)
    {
        Next = next;
        Previous = previous;
        Turn = turn;
    }
}
