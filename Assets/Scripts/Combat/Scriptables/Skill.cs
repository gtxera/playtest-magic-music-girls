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
    
    [field: SerializeField]
    public bool DeadTarget { get; private set; }
    
    public abstract TargetType TargetType { get; }
    
    
    public IEnumerable<StatScaling> BaseScalings => _baseScalings;

    public float GetScaledValue(Stats stats)
    {
        var finalValue = BaseValue;

        foreach (var scaling in _baseScalings)
        {
            finalValue += scaling.GetValue(stats);
        }

        return finalValue;
    }

    public abstract void Execute(Unit unit, IEnumerable<Unit> targets);
}

public enum TargetType
{
    Ally,
    Opposite
}