using UnityEngine;

public class StatQuery
{
    public readonly Stat Stat;

    public float Value;

    public StatQuery(Stat stat, float value)
    {
        Stat = stat;
        Value = value;
    }
}
