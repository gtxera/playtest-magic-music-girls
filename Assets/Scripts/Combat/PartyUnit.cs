using System.Collections.Generic;
using UnityEngine;

public class PartyUnit : Unit
{
    [SerializeField]
    private PartyCharacterData _characterData;

    public Sprite Icon => _characterData.Icon;

    private void Awake()
    {
        Character = Party.Instance.GetFromData(_characterData);
    }
    
    protected override void OnTurnPassed(Unit currentUnit)
    {
        if (currentUnit != this)
            return;
    }
}
