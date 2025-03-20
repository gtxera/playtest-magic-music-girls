using System;
using System.Collections.Generic;
using System.Linq;
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
    public int CooldownInTurns { get; private set; }
    
    [field: SerializeField]
    public SkillPriorityType PriorityType { get; private set; }
    
    [field: SerializeField]
    public int Priority { get; private set; }
    
    public abstract TargetType TargetType { get; }

    public abstract SelectionFlags SelectionFlags { get; }

    public abstract SelectionFlags UnselectableFlags { get; }

    [field: SerializeField]
    public TargetSelectionStrategy TargetSelectionStrategy { get; private set; }
    
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

    public void Execute(Unit unit, IEnumerable<Unit> targets)
    {
        var targetsArray = targets as Unit[] ?? targets.ToArray();
        var targetsCount = targetsArray.Length;
        if (targetsCount > MaxTargets)
            throw new InvalidOperationException(
                $"A habilidade {BaseName} foi chamada com {targetsCount} alvos e possui um maximo de {MaxTargets}");

        foreach (var target in targetsArray)
        {
            ExecuteForTarget(unit, target);
        }
    }

    public abstract void ExecuteForTarget(Unit unit, Unit target);
}

public enum TargetType
{
    Ally,
    Opposite
}

public enum SkillPriorityType
{
    ChanceFromPriority,
    UseWhenAvailable
}

[Flags]
public enum SelectionFlags
{
    Alive = 1 << 0,
    Dead = 1 << 1,
    FullHealth = 1 << 2
}
