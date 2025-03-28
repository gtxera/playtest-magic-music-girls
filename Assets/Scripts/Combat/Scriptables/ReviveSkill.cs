using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ReviveSkill", menuName = "Scriptable Objects/Skills/ReviveSkill")]
public class ReviveSkill : Skill
{
    public override TargetType TargetType => TargetType.Ally;

    public override SelectionFlags MandatoryFlags => 0;

    public override SelectionFlags ForbiddenFlags => SelectionFlags.Alive;

    protected override float ExecuteForTarget(Unit unit, Unit target)
    {
        return unit.Heal(target, GetScaledValue(unit.Stats));
    }
}
