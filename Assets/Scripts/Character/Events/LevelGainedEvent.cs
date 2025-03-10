using UnityEngine;

public class LevelGainedEvent : IEvent
{
    public readonly int LevelBefore;
    public readonly int LevelAfter;

    public LevelGainedEvent(int levelBefore, int levelAfter)
    {
        LevelBefore = levelBefore;
        LevelAfter = levelAfter;
    }
}
