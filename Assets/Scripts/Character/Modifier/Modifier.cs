using System;
using UnityEngine;

[Serializable]
public abstract class Modifier
{
    [field: SerializeField]
    public string Identifier { get; private set; }

    [field: SerializeField]
    public ModifierType Type { get; private set; }
    
    [field: SerializeField]
    public float ModifyValue { get; private set; }

    public float GetValueToAdd(float valueToModify, ModifyParameters parameters)
    {
        if (!ShouldModify(parameters))
            return 0;

        return Type switch
        {
            ModifierType.Simple => ModifyValue,
            ModifierType.PercentageOfBaseValue => valueToModify * ModifyValue,
            ModifierType.FromParameter => GetValueFromParameter(valueToModify, parameters),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    protected virtual float GetValueFromParameter(float initialValue, ModifyParameters parameters)
    {
        return 0;
    }

    protected abstract bool ShouldModify(ModifyParameters parameters);

    public override bool Equals(object obj)
    {
        if (obj is not Modifier modifier) 
            return false;

        return Identifier == modifier.Identifier;
    }

    public override int GetHashCode()
    {
        return Identifier.GetHashCode();
    }
}
