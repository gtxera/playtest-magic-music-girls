using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject, ICombatCommand
{
    [Header("Skill Base Stats")]
    [field: SerializeField]
    public string BaseName { get; private set; }
    [field: SerializeField]
    public string BaseDescription { get; private set; }
    [field: SerializeField]
    public float BaseValue { get; private set; }
    [field: SerializeField] 
    public int MaxTargets { get; private set; } = 1;
    [SerializeField]
    private StatScaling[] _baseScalings;
    [field: SerializeField]
    public Skill EvolvedSkill { get; private set; }
    
    public abstract TargetType TargetType { get; }
    
    public IEnumerable<StatScaling> BaseScalings => _baseScalings;

    public abstract void Execute(Unit unit, IEnumerable<Unit> targets);
}

[Serializable]
public class StatScaling : Modifier
{
    [field: SerializeField]
    public Stat Stat { get; private set; }
    
    [field: SerializeField]
    public float Scaling { get; private set; }
    
    [field: SerializeField]
    public ModifierType ScalingType { get; private set; }

    public StatScaling(string identifier, ModifierType type) : base(identifier, type)
    {
        identifier = $"{Stat}-{Scaling}-{ScalingType}";
    }

    public override float Modify(float currentValue, ModifyParameters parameters)
    {
        throw new NotImplementedException();
    }
}

public enum TargetType
{
    Ally,
    Opposite
}