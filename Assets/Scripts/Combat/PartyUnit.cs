using UnityEngine;

public class PartyUnit : Unit, IEventListener<CombatTurnPassedEvent>
{
    [SerializeField]
    private CharacterData characterData;

    public void Handle(CombatTurnPassedEvent @event)
    {
        throw new System.NotImplementedException();
    }

    private void Awake()
    {
        Character = Party.Instance.GetFromData(characterData);
    }
}
