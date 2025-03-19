using UnityEngine;

public class SelectionChangedEvent : IEvent
{
    public readonly int TargetsCount;
    public readonly int RemainingTargetsCount;
    public bool FinishedSelecting => RemainingTargetsCount == 0;
    public readonly bool AllySelection;

    public SelectionChangedEvent(int targetsCount, int remainingTargetsCount, bool allySelection)
    {
        TargetsCount = targetsCount;
        RemainingTargetsCount = remainingTargetsCount;
        AllySelection = allySelection;
    }
}
