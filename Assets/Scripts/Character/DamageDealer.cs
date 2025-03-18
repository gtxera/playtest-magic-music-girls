using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DamageDealer
{
    private readonly Stats _stats;
    private readonly Modifiers<DamageDealerModifier> _modifiers;

    public DamageDealer(Stats stats)
    {
        _stats = stats;
    }
    
    public float CalculateDamage(float initialDamage, IEnumerable<StatScaling> scalings)
    {
        scalings = scalings.OrderBy(s => s.ScalingType);

        foreach (var scaling in scalings)
        {
            var valueToModify = _stats.Get(scaling.Stat) * scaling.Scaling;

            switch (scaling.ScalingType)
            {
                case ModifierType.Additive:
                    initialDamage += valueToModify;
                    break;
                case ModifierType.Multiplicative:
                    initialDamage *= valueToModify;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        return _modifiers.GetModified(initialDamage, null);
    }

    public void AddModifier(DamageDealerModifier modifier)
    {
        _modifiers.Add(modifier);
    }
}