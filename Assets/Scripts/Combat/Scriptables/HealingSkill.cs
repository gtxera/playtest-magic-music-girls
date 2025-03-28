using System;
using UnityEngine;

[CreateAssetMenu(fileName = "HealingSkill", menuName = "Scriptable Objects/Skills/HealingSkill")]
public class HealingSkill : Skill
{
    public override TargetType TargetType => TargetType.Ally;

    public override SelectionFlags MandatoryFlags => SelectionFlags.Alive;

    public override SelectionFlags ForbiddenFlags => SelectionFlags.FullHealth;

    protected override float ExecuteForTarget(Unit unit, Unit target)
    {
        return unit.Heal(target, GetScaledValue(unit.Stats));
    }
}
