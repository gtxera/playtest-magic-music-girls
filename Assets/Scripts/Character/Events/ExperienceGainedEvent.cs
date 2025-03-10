using UnityEngine;

public class ExperienceGainedEvent : IEvent
{
    private readonly float ExperienceBefore;
    private readonly float ExperienceAfter;
    private float ChangeAmount => ExperienceAfter - ExperienceBefore;

    public ExperienceGainedEvent(float experienceBefore, float experienceAfter)
    {
        ExperienceBefore = experienceBefore;
        ExperienceAfter = experienceAfter;
    }
}
