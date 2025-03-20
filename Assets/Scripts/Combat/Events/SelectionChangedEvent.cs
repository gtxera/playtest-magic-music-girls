using UnityEngine;

public class SelectionChangedEvent : IEvent
{
    public readonly bool HasTargets;
    public readonly int TargetsCount;
    public readonly int RemainingTargetsCount;
    public bool FinishedSelecting => RemainingTargetsCount == 0;
    public readonly bool AllySelection;

    public SelectionChangedEvent(int targetsCount, int remainingTargetsCount, bool allySelection, bool hasTargets)
    {
        HasTargets = hasTargets;
        TargetsCount = targetsCount;
        RemainingTargetsCount = remainingTargetsCount;
        AllySelection = allySelection;
    }
}
