public class StatModifier : Modifier
{
    public Stat Stat { get; private set; }

    public float ModifyValue { get; private set; }

    public const string STAT_PARAMETER_KEY = "stat";

    public StatModifier(string identifier, ModifierType type, Stat stat, float modifyValue) : base(identifier, type)
    { 
        Stat = stat;
        ModifyValue = modifyValue;
    }

    public override float Modify(float currentValue, ModifyParameters parameters)
    {
        var modifiedStat = parameters.Get<Stat>(STAT_PARAMETER_KEY);

        if (modifiedStat != Stat)
            return currentValue;

        return Type switch
        {
            ModifierType.Additive => currentValue + ModifyValue,
            ModifierType.Multiplicative => currentValue * ModifyValue,
            _ => currentValue
        };
    }
}
