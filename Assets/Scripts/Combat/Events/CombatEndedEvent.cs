using UnityEngine;

public class CombatEndedEvent : IEvent
{
    public readonly bool PlayerVictory;

    public CombatEndedEvent(bool playerWon)
    {
        PlayerVictory = playerWon;
    }
}
