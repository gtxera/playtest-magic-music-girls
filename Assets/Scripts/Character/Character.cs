using System.Collections.Generic;
using UnityEngine;

public abstract class Character
{
    private readonly DamageMitigator _damageMitigator;
    private readonly DamageDealer _damageDealer;
    
    protected Character(CharacterData characterData)
    {
        CharacterData = characterData;
        BaseStats = characterData.BaseStats.Copy();
        Stats = new Stats(BaseStats);
        Health = new Health(Stats);

        _damageMitigator = new DamageMitigator(Stats);
        _damageDealer = new DamageDealer(Stats);
    }

    public CharacterData CharacterData { get; }
    protected BaseStats BaseStats { get; }
    public Stats Stats { get; }
    public Health Health { get; }
    public abstract int Level { get; }

    public float DealDamage(Character target, float initialDamage, IEnumerable<StatScaling> scalings)
    {
        return target.TakeDamage(_damageDealer.CalculateDamage(initialDamage));
    }

    private float TakeDamage(float damage)
    {
        var actualDamage = _damageMitigator.Mitigate(damage);
        Health.Damage(actualDamage);
        return actualDamage;
    }

    public void TakeRawDamage(float damage)
    {
        Health.Damage(damage);
    }

    public void Heal(float heal)
    {
        Health.Heal(heal);
    }

    public void AddModifier(Modifier modifier)
    {
        switch (modifier)
        {
            case StatModifier statModifier:
                Stats.AddModifier(statModifier);
                break;
            
            case DamageDealerModifier damageDealerModifier:
                _damageDealer.AddModifier(damageDealerModifier);
                break;
            
            case DamageMitigatorModifier damageMitigatorModifier:
                _damageMitigator.AddModifier(damageMitigatorModifier);
                break;
            
            default:
                Debug.LogError("Tipo de modificador nao implementado");
                break;
        }
    }

    public override bool Equals(object obj)
    {
        if (obj is not Character character)
            return false;

        return character.CharacterData.Equals(CharacterData);
    }

    public override int GetHashCode()
    {
        return CharacterData.GetHashCode();
    }

    public abstract IEnumerable<Skill> GetSkills();
}
