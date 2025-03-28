using System.Collections.Generic;

public class CharacterAddedToPartyEvent : IEvent
{
    public readonly PartyCharacter AddedCharacter;

    public CharacterAddedToPartyEvent(PartyCharacter character)
    {
        AddedCharacter = character;
    }
}
