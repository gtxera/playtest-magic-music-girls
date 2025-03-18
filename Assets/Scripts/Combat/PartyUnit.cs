using System.Collections.Generic;
using UnityEngine;

public class PartyUnit : Unit, IEventListener<CombatTurnPassedEvent>
{
    [SerializeField]
    private CharacterData _characterData;

    public void Handle(CombatTurnPassedEvent @event)
    {
    }

    private void Awake()
    {
        Character = Party.Instance.GetFromData(_characterData);
    }
}
