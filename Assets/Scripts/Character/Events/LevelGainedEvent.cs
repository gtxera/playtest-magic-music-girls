using UnityEngine;

public class LevelGainedEvent : IEvent
{
    public readonly int LevelBefore;
    public readonly int LevelAfter;
    public int LevelsGained => LevelAfter - LevelBefore;

    public LevelGainedEvent(int levelBefore, int levelAfter)
    {
        LevelBefore = levelBefore;
        LevelAfter = levelAfter;
    }
}
