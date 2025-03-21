using System.Collections.Generic;
using UnityEngine;

public class CombatEndedEvent : IEvent
{
    public readonly bool PlayerVictory;
    public readonly float ExperienceReward;
    public readonly float MoneyReward;
    public readonly Component EncounterStarter;
    public readonly IEnumerable<Loot> Loot;

    public CombatEndedEvent(bool playerWon, float experienceReward, float moneyReward, Component encounterStarter, IEnumerable<Loot> loot)
    {
        PlayerVictory = playerWon;
        ExperienceReward = experienceReward;
        MoneyReward = moneyReward;
        EncounterStarter = encounterStarter;
        Loot = loot;
    }
}
