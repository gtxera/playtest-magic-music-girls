using System;
using UnityEngine;

[Serializable]
public class StatModifier : Modifier
{
    [field: SerializeField]
    public Stat Stat { get; private set; }

    public const string STAT_PARAMETER_KEY = "stat";

    protected override bool ShouldModify(ModifyParameters parameters) => ShouldModify(parameters, Stat);

    public static bool ShouldModify(ModifyParameters parameters, Stat stat)
    {
        var modifiedStat = parameters.Get<Stat>(STAT_PARAMETER_KEY);

        return modifiedStat == stat;
    }
}
