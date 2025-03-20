using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "HealingSkill", menuName = "Scriptable Objects/Skills/HealingSkill")]
public class HealingSkill : Skill
{
    public override TargetType TargetType => TargetType.Ally;


    public override void ExecuteForTarget(Unit unit, Unit target)
    {
        target.Heal(GetScaledValue(unit.Stats));
    }
}
