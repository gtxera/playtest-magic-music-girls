using UnityEngine;

public class CombatTurnPassedEvent : IEvent
{
    public readonly int Turn;
    public readonly Unit Unit;

    public CombatTurnPassedEvent(Unit unit, int turn)
    {
        Unit = unit;
        Turn = turn;
    }
}
