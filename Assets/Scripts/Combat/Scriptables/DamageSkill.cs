using UnityEngine;

[CreateAssetMenu(fileName = "DamageSkill", menuName = "Scriptable Objects/Skills/DamageSkill")]
public class DamageSkill : Skill
{
    public override TargetType TargetType => TargetType.Opposite;

    public override void ExecuteForTarget(Unit unit, Unit target)
    {
        unit.DealDamage(target, GetScaledValue(unit.Stats));
    }
}
