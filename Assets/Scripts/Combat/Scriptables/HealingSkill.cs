using System;
using UnityEngine;

[CreateAssetMenu(fileName = "HealingSkill", menuName = "Scriptable Objects/Skills/HealingSkill")]
public class HealingSkill : Skill
{
    public override TargetType TargetType => TargetType.Ally;

    public override SelectionFlags MandatoryFlags => SelectionFlags.Alive;

    public override SelectionFlags ForbiddenFlags => SelectionFlags.FullHealth;

    public override void ExecuteForTarget(Unit unit, Unit target)
    {
        target.Heal(GetScaledValue(unit.Stats));
    }
}
