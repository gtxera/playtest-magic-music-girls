using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public abstract class Modifier
{
    public string Identifier { get; private set; }

    [field: SerializeField]
    public ModifierType Type { get; private set; }
    
    public float ModifyValue { get; private set; }
    
    [field: SerializeField]
    public int Duration { get; private set; }

    private Unit _creator;
    private Unit _holder;
    private int _turnOfCreation;

    public Modifier CreateCopy(string identifier, float modifyValue, Unit creator, Unit holder, int currentTurn)
    {
        var modifier = CreateCopy();

        modifier.ModifyValue = modifyValue;
        modifier.Identifier = identifier;
        modifier.Duration = Duration;
        modifier.Type = Type;
        
        modifier._creator = creator;
        modifier._holder = holder;
        modifier._turnOfCreation = currentTurn;

        return modifier;
    }

    protected abstract Modifier CreateCopy();
    
    public float GetValueToAdd(float valueToModify, ModifyParameters parameters)
    {
        if (!ShouldModify(parameters))
            return 0;

        return Type switch
        {
            ModifierType.Simple => ModifyValue,
            ModifierType.PercentageOfBaseValue => valueToModify * (ModifyValue / 100),
            ModifierType.FromParameter => GetValueFromParameter(valueToModify, parameters),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    
    protected virtual float GetValueFromParameter(float initialValue, ModifyParameters parameters)
    {
        return 0;
    }

    protected abstract bool ShouldModify(ModifyParameters parameters);

    public bool IsFinished(Unit unit, int currentTurn)
    {
        if (Duration <= 0)
            return false;

        return _turnOfCreation + Duration >= currentTurn && unit == _holder;
    }
    
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
