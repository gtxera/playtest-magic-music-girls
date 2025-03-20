using UnityEngine;

[CreateAssetMenu(fileName = "ModifierSkill", menuName = "Scriptable Objects/Skills/ModifierSkill")]
public class ModifierSkill : Skill
{
    [SerializeField]
    private ModifierType _modifierType;
    
    [SerializeField]
    private TargetType _targetType;

    [SerializeField]
    private int _durationInTurns;
    
    public override TargetType TargetType => _targetType;

    [SerializeField]
    private CombatStatModifier _combatStatModifier;

    public override void ExecuteForTarget(Unit unit, Unit target)
    {
        target.AddModifier(_combatStatModifier);
    }
}
