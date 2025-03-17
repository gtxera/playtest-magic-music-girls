using UnityEngine;

public class PartyCharacter : Character
{
    private readonly Party _party;

    private readonly PartyUnit _partyUnit;

    public PartyCharacter(CharacterData characterData, Party party) : base(characterData)
    {
        _party = party;
        _partyUnit = (PartyUnit)characterData.CombatPrefab;
    }

    public override int Level => _party.Level;

    public PartyUnit PartyUnit => _partyUnit;
}
