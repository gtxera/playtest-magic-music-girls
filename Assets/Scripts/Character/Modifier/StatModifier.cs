using System;
using UnityEngine;

[Serializable]
public class StatModifier : Modifier
{
    [field: SerializeField]
    public Stat Stat { get; private set; }

    public const string STAT_PARAMETER_KEY = "stat";

    protected override Modifier CreateCopy()
    {
        var modifier = new StatModifier
        {
            Stat = Stat
        };

        return modifier;
    }

    protected override bool ShouldModify(ModifyParameters parameters)
    {
        var modifiedStat = parameters.Get<Stat>(STAT_PARAMETER_KEY);

        return modifiedStat == Stat;
    }
}
