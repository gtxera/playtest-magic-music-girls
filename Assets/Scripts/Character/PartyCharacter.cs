using UnityEngine;

public class PartyCharacter : Character, IEventListener<LevelGainedEvent>
{
    private readonly Party _party;

    private readonly PartyUnit _partyUnit;

    public PartyCharacter(PartyCharacterData characterData, Party party) : base(characterData)
    {
        _party = party;
        _partyUnit = characterData.CombatPrefab;
        EventBus.Instance.Subscribe(this);
    }

    public override int Level => _party.Level;

    public PartyUnit PartyUnit => _partyUnit;
    
    public void Handle(LevelGainedEvent @event)
    {
        
    }
}
