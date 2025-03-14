using UnityEngine;

public interface ICombatModifier
{
    public int DurationInTurns { get; protected set; }

    public int TurnOfCreation { get; protected set; }

    public Unit CreatedBy { get; protected set; }

    public bool Finished(Unit currentUnit, int currentTurn)
    {
        return currentTurn - DurationInTurns > TurnOfCreation + 1 && currentUnit == CreatedBy;
    }
}
