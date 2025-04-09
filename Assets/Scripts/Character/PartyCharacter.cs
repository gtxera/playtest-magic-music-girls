using System.Collections.Generic;
using UnityEngine;

public class PartyCharacter : Character, IEventListener<LevelGainedEvent>
{
    private readonly Party _party;

    private readonly PartyUnit _partyUnitPrefab;

    public readonly PartyCharacterData PartyCharacterData;

    private EquipmentItem _firstSlot;
    private EquipmentItem _secondSlot;

    public PartyCharacter(PartyCharacterData characterData, Party party) : base(characterData)
    {
        PartyCharacterData = characterData;
        _party = party;
        _partyUnitPrefab = characterData.CombatPrefab;
        EventBus.Instance.Subscribe(this);
    }

    public override int Level => _party.Level;

    public PartyUnit PartyUnitPrefab => _partyUnitPrefab;

    public void Handle(LevelGainedEvent @event)
    {
        BaseStats.SetForLevel(@event.LevelAfter, CharacterData.BaseStats, PartyCharacterData.StatsGrowth);
    }
}
