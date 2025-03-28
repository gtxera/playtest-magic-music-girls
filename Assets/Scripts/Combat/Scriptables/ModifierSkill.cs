using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "ModifierSkill", menuName = "Scriptable Objects/Skills/ModifierSkill")]
public class ModifierSkill : Skill
{
    [SerializeField]
    private TargetType _targetType;

    [SerializeField]
    private float _energyGenerated;
    
    public override TargetType TargetType => _targetType;

    public override SelectionFlags MandatoryFlags => SelectionFlags.Alive;

    public override SelectionFlags ForbiddenFlags => 0;

    [SerializeReference]
    private Modifier _modifier;

    protected override float ExecuteForTarget(Unit unit, Unit target)
    {
        var modifier = _modifier.CreateCopy(
            BaseName,
            GetScaledValue(unit.Stats),
            unit,
            target,
            CombatManager.Instance.CurrentTurn);
        
        return unit.AddModifier(target, modifier, _energyGenerated);
    }

    public override string GetDescription()
    {
        var builder = new StringBuilder(base.GetDescription());
        builder.AppendLine();
        builder.Append($"Duração: {GetDurationText()}");

        return builder.ToString();
    }

    private string GetDurationText()
    {
        if (_modifier.Duration <= 0)
            return "Sempre";

        return $"{_modifier.Duration} {(_modifier.Duration > 1 ? "turnos" : "turno")}";
    }
}
