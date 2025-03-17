using System.Collections.Generic;

public class CombatEndedEvent : IEvent
{
    public readonly bool PlayerVictory;
    public readonly float ExperienceReward;
    public readonly float MoneyReward;
    public IEnumerable<Loot> Loot;

    public CombatEndedEvent(bool playerWon, float experienceReward, float moneyReward, IEnumerable<Loot> loot)
    {
        PlayerVictory = playerWon;
        ExperienceReward = experienceReward;
        MoneyReward = moneyReward;
        Loot = loot;
    }
}
