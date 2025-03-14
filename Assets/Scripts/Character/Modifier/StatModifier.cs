public class StatModifier : Modifier
{
    public Stat Stat { get; private set; }

    public float ModifyValue { get; private set; }

    public const string STAT_PARAMETER_KEY = "stat";

    public StatModifier(Stat stat, float modifyValue, ModifierType type) : base(type)
    { 
        Stat = stat;
        ModifyValue = modifyValue;
    }

    public override float Modify(float currentValue, ModifyParameters parameters)
    {
        var modifiedStat = parameters.Get<Stat>(STAT_PARAMETER_KEY);

        if (modifiedStat != Stat)
            return currentValue;

        if (Type == ModifierType.Additive)
            return currentValue + ModifyValue;
        else if (Type == ModifierType.Multiplicative)
            return currentValue + ModifyValue;

        return currentValue;
    }
}
