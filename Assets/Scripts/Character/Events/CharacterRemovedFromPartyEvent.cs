public class CharacterRemovedFromPartyEvent : IEvent
{
    public readonly PartyCharacter RemovedCharacter;
    
    public CharacterRemovedFromPartyEvent(PartyCharacter character)
    {
        RemovedCharacter = character;
    }
}
