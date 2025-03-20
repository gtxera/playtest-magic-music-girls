using System;
using UnityEngine;

[Serializable]
public abstract class CombatModifier : Modifier
{
    [field: SerializeField]
    public int DurationInTurns { get; private set; }
    
    public int TurnOfCreation { get; private set; }
    public Unit CreatedBy { get; private set; }

    public bool Finished(Unit currentUnit, int currentTurn)
    {
        return currentTurn - DurationInTurns > TurnOfCreation + 1 && currentUnit == CreatedBy;
    }
}
