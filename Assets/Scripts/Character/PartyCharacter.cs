using System.Collections.Generic;
using UnityEngine;

public class PartyCharacter : Character, IEventListener<LevelGainedEvent>
{
    private readonly Party _party;

    private readonly PartyUnit _partyUnitPrefab;

    public PartyCharacter(PartyCharacterData characterData, Party party) : base(characterData)
    {
        _party = party;
        _partyUnitPrefab = characterData.CombatPrefab;
        EventBus.Instance.Subscribe(this);
    }

    public override int Level => _party.Level;

    public PartyUnit PartyUnitPrefab => _partyUnitPrefab;

    public override IEnumerable<Skill> GetSkills()
    {
        return CharacterData.Skills;
    }

    public void Handle(LevelGainedEvent @event)
    {
        
    }
}
