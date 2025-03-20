using System;
using UnityEngine;

public class EnemyUnit : Unit
{
    [field: SerializeField]
    public EnemyCharacterData Data { get; private set; }

    public override Sprite Icon => Data.Icon;
    
    private void Awake()
    {
        Character = new EnemyCharacter(Data);
    }
    protected override void OnTurnPassed(Unit currentUnit)
    {
        if (currentUnit != this)
            return;
    }
}
