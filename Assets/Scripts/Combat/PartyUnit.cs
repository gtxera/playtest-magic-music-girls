using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PartyUnit : Unit
{
    [SerializeField]
    private PartyCharacterData _characterData;

    [SerializeField]
    private string _instrumentKey;

    private PartyCharacter _partyCharacter;

    public override Sprite Icon => _characterData.Icon;

    private void Awake()
    {
        _partyCharacter = Party.Instance.GetFromData(_characterData);
        Character = _partyCharacter;
        
        if (IsDead)
            Character.HealRaw(5);
    }

    public override float DealDamage(Unit target, float initialDamage)
    {
        var damage = base.DealDamage(target, initialDamage);
        
        var energy = target.GetPercentageOfMaxHealth(damage) * _characterData.EnergyGenerationPercentages.DamagePercentage;
        return energy;
    }

    public override float Heal(Unit target, float heal)
    {
        var finalHeal = base.Heal(target, heal);
        
        var energy = target.GetPercentageOfMaxHealth(finalHeal) * _characterData.EnergyGenerationPercentages.HealingPercentage;
        return energy;
    }

    public override float AddModifier(Unit target, Modifier modifier, float energy, string animationKey)
    {
        return base.AddModifier(target, modifier, energy, animationKey) * _characterData.EnergyGenerationPercentages.ModifierPercentage;
    }

    public void PlayMusic(float duration)
    {
        CombatMusic.Instance.PlayInstrument(_instrumentKey, duration);
    }

    protected override IEnumerable<Skill> GetSkills()
    {
        return SkillsManager.Instance.GetSelection(_partyCharacter);
    }

    protected override UniTask OnUnitTurn()
    {
        return UniTask.CompletedTask;
    }
}
