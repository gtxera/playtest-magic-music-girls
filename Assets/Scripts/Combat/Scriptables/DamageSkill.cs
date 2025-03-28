using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageSkill", menuName = "Scriptable Objects/Skills/DamageSkill")]
public class DamageSkill : Skill
{
    public override TargetType TargetType => TargetType.Opposite;

    public override SelectionFlags MandatoryFlags => SelectionFlags.Alive;

    public override SelectionFlags ForbiddenFlags => 0;

    protected override float ExecuteForTarget(Unit unit, Unit target)
    {
        return unit.DealDamage(target, GetScaledValue(unit.Stats));
    }
}
