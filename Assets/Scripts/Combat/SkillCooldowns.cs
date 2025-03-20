using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SkillCooldowns
{
    private readonly IEnumerable<SkillCooldown> _skillCooldowns;

    public SkillCooldowns(IEnumerable<Skill> skills)
    {
        _skillCooldowns = skills.Select(s => new SkillCooldown(s));
    }

    public IEnumerable<SkillCooldown> GetSkills() => _skillCooldowns;
    public IEnumerable<Skill> GetAvailableSkills() => _skillCooldowns.Where(s => s.Available).Select(s => s.Skill);

    public void UpdateCooldowns()
    {
        foreach (var skill in _skillCooldowns)
        {
            skill.UpdateCooldown();
        }
    }
}

public class SkillCooldown
{
    public int RemainingCooldown { get; private set; }
    
    public Skill Skill { get; }
    public bool Available => RemainingCooldown == 0;

    public SkillCooldown(Skill skill)
    {
        Skill = skill;
    }

    public void Use()
    {
        RemainingCooldown = Skill.CooldownInTurns;
    }

    public void UpdateCooldown()
    {
        if (RemainingCooldown > 0)
            RemainingCooldown--;
    }

    public string GetUseIntervalText()
    {
        if (!Available)
            return $"Intervalo restante: {RemainingCooldown} turnos";

        var builder = new StringBuilder("Intervalo de uso: ");
        
        builder.Append(Skill.CooldownInTurns == 0 ? "Sempre" : $"{Skill.CooldownInTurns} turnos");

        return builder.ToString();
    }
}