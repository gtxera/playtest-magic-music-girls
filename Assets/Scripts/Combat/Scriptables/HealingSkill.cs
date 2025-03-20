using System;
using UnityEngine;

[CreateAssetMenu(fileName = "HealingSkill", menuName = "Scriptable Objects/Skills/HealingSkill")]
public class HealingSkill : Skill
{
    public override TargetType TargetType => TargetType.Ally;

    public override SelectionFlags SelectionFlags => SelectionFlags.Alive;

    public override SelectionFlags UnselectableFlags => throw new NotImplementedException();

    public override void ExecuteForTarget(Unit unit, Unit target)
    {
        target.Heal(GetScaledValue(unit.Stats));
    }
}
