using System;
using UnityEngine;

[Serializable]
public class CombatStatModifier : CombatModifier
{
    [field: SerializeField]
    public Stat Stat { get; private set; }
    
    protected override bool ShouldModify(ModifyParameters parameters) => StatModifier.ShouldModify(parameters, Stat);
}
