using System;using System.Collections.Generic;using UnityEngine;

public abstract class StatModifier
{
    public abstract int Priority { get; }
    
    protected abstract Stat Stat { get; }

    protected abstract float Modify(float baseValue);

    public void Handle(StatQuery query)
    {
        if (query.Stat != Stat)
            return;

        query.Value = Modify(query.Value);
    }
}

public class StatModifierComparer : IComparer<StatModifier>
{
    public int Compare(StatModifier x, StatModifier y)
    {
        if (ReferenceEquals(x, y)) return 0;
        if (ReferenceEquals(null, y)) return 1;
        if (ReferenceEquals(null, x)) return -1;
        return x.Priority.CompareTo(y.Priority);
    }
}
