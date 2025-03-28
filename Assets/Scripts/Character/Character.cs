using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public abstract class Character
{
    private readonly DamageMitigator _damageMitigator;
    private readonly DamageDealer _damageDealer;
    private readonly HealingDealer _healingDealer;
    private readonly HealingReceiver _healingReceiver;
    
    protected Character(CharacterData characterData)
    {
        CharacterData = characterData;
        BaseStats = characterData.BaseStats.Copy();
        Stats = new Stats(BaseStats);
        Health = new Health(Stats);

        _damageMitigator = new DamageMitigator(Stats);
        _damageDealer = new DamageDealer(Stats);
        _healingDealer = new HealingDealer(Stats);
        _healingReceiver = new HealingReceiver(Stats);
    }

    public CharacterData CharacterData { get; }
    protected BaseStats BaseStats { get; }
    public Stats Stats { get; }
    public Health Health { get; }
    public abstract int Level { get; }

    public float DealDamage(Character target, float initialDamage)
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

    public float HealOhter(Character target, float initialHeal)
    {
        return target.Heal(_healingDealer.CalculateHeal(initialHeal));
    }

    public float Heal(float heal)
    {
        var actualHeal = _healingReceiver.CalculateHeal(heal);
        Health.Heal(actualHeal);
        return actualHeal;
    }

    public void HealRaw(float heal)
    {
        Health.Heal(heal);
    }

    public void AddModifier(Modifier modifier)
    {
        float baseValue;
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
            
            case HealingDealerModifier healingModifier:
                _healingDealer.AddModifier(healingModifier);
                break;

            default:
                throw new NotImplementedException("Modificador nao implementado");
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
}
