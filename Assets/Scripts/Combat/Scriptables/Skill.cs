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

    public IReadOnlyCollection<StatScaling> BaseScalings => _baseScalings;

    public abstract void Execute(Unit unit, IEnumerable<Unit> targets);
}

[Serializable]
public class StatScaling
{
    [field: SerializeField]
    public Stat Stat { get; private set; }
    
    [field: SerializeField]
    public float Scaling { get; private set; }
    
    [field: SerializeField]
    public ModifierType ScalingType { get; private set; }
}