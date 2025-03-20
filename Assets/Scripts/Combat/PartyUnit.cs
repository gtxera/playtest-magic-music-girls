using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PartyUnit : Unit
{
    [SerializeField]
    private PartyCharacterData _characterData;


    public override Sprite Icon => _characterData.Icon;

    private void Awake()
    {
        Character = Party.Instance.GetFromData(_characterData);
    }
    
    protected override UniTask OnUnitTurn()
    {
        return UniTask.CompletedTask;
    }
}
