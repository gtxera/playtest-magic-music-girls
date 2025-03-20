using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageSkill", menuName = "Scriptable Objects/Skills/DamageSkill")]
public class DamageSkill : Skill
{
    public override TargetType TargetType => TargetType.Opposite;

    public override SelectionFlags SelectionFlags => SelectionFlags.Alive;

    public override SelectionFlags UnselectableFlags => 0;

    public override void ExecuteForTarget(Unit unit, Unit target)
    {
        unit.DealDamage(target, GetScaledValue(unit.Stats));
    }
}
