using System.Collections.Generic;
using UnityEngine;

public class StatsMediator
{
    private readonly List<StatModifier> _modifiers;

    public void Add(StatModifier modifier)
    {
        _modifiers.Add(modifier);
        _modifiers.Sort(new StatModifierComparer());
    }
}
