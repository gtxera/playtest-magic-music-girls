using System.Collections.Generic;

public class CombatEndedEvent : IEvent
{
    public readonly bool PlayerVictory;
    public readonly float ExperienceReward;
    public readonly float MoneyReward;
    public readonly EncounterStarter EncounterStarter;
    public readonly IEnumerable<Loot> Loot;

    public CombatEndedEvent(bool playerWon, float experienceReward, float moneyReward, EncounterStarter encounterStarter, IEnumerable<Loot> loot)
    {
        PlayerVictory = playerWon;
        ExperienceReward = experienceReward;
        MoneyReward = moneyReward;
        EncounterStarter = encounterStarter;
        Loot = loot;
    }
}
