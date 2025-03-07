using UnityEngine;

public abstract class Character
{
    private readonly DamageMitigator _damageMitigator;
    
    protected Character(CharacterData characterData)
    {
        CharacterData = characterData;
        Stats = new Stats(new StatsMediator(), CharacterData.BaseStats);
        Health = new Health(Stats);
    }

    public CharacterData CharacterData { get; }
    public Stats Stats { get; }
    public Health Health { get; }
    public abstract int Level { get; }

    public void TakeDamage(float damage)
    {
        Health.Damage(_damageMitigator.Mitigate(damage));
    }

    public void TakeRawDamage(float damage)
    {
        Health.Damage(damage);
    }

    public void Heal(float heal)
    {
        Health.Heal(heal);
    }

    public override bool Equals(object obj)
    {
        if (obj is not Character character || ReferenceEquals(this, null))
            return false;

        return character.CharacterData.Equals(CharacterData);
    }

    public override int GetHashCode()
    {
        return CharacterData.GetHashCode();
    }
}
