using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class Skill : ScriptableObject, ICombatCommand
{
    [Header("Skill Base Stats")]
    [field: SerializeField]
    public string BaseName { get; private set; }

    [field: SerializeField]
    private string _baseDescription;
    
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
    public int Priority { get; private set; } = 1;

    [field: SerializeField]
    public Sprite Icon { get; private set; }
    
    public abstract TargetType TargetType { get; }

    public abstract SelectionFlags MandatoryFlags { get; }

    public abstract SelectionFlags ForbiddenFlags { get; }

    [field: SerializeField]
    public TargetSelectionStrategy TargetSelectionStrategy { get; private set; }
    
    public IEnumerable<StatScaling> BaseScalings => _baseScalings;

    public string Name => BaseName;

    [field: SerializeField]
    public ComboEmotion ComboEmotion { get; private set; }

    [field: SerializeField]
    public SkillType SkillType { get; private set; }

    [field: SerializeField]
    public string AnimationKey { get; private set; }

    public virtual string GetDescription() => _baseDescription;

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

        unit.UseSkill(this);

        var energy = 0f;
        foreach (var target in targetsArray)
        {
            energy += ExecuteForTarget(unit, target);
        }
        
        GenerateEnergy(unit, energy);
    }

    protected abstract float ExecuteForTarget(Unit unit, Unit target);

    private void GenerateEnergy(Unit unit, float energy)
    {
        if (unit.GetType() != typeof(PartyUnit))
            return;
        
        if (SkillType is not (SkillType.Normal or SkillType.Item))
            return;
        
        CombatManager.Instance.ComboManager.GenerateEnergy(energy);
    }
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

public enum SkillType
{
    Normal,
    Evolved,
    Combo,
    Item
}