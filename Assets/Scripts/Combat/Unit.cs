using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    protected Character Character;

    public Stats Stats => Character.Stats;
    public Health Health => Character.Health;
    
    public void DealDamage(Unit target, float initialDamage)
    {
        Character.DealDamage(target.Character, initialDamage);
    }
    
    private void TakeDamage(float damage)
    {
        Character.TakeDamage(damage);
    }

    public void Heal(float heal)
    {
        Character.Heal(heal);
    }

    public void AddModifier(Modifier modifier)
    {
        Character.AddModifier(modifier);
    }
}
