using UnityEngine;

public class PartyCharacter : Character
{
    private readonly Party _party;

    public PartyCharacter(CharacterData characterData, Party party) : base(characterData)
    {
        _party = party;
    }

    public override int Level => _party.Level;
}
