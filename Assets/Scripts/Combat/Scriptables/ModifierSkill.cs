using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "ModifierSkill", menuName = "Scriptable Objects/Skills/ModifierSkill")]
public class ModifierSkill : Skill
{
    [SerializeField]
    private TargetType _targetType;
    
    public override TargetType TargetType => _targetType;

    public override SelectionFlags MandatoryFlags => SelectionFlags.Alive;

    public override SelectionFlags ForbiddenFlags => 0;

    [SerializeReference]
    private Modifier _modifier;

    protected override void ExecuteForTarget(Unit unit, Unit target)
    {
        unit.AddModifier(target, _modifier.CreateCopy(BaseName, GetScaledValue(unit.Stats), unit, target, CombatManager.Instance.CurrentTurn));
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
