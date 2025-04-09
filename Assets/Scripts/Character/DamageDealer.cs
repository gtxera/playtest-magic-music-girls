public class DamageDealer
{
    private readonly Stats _stats;
    private readonly Modifiers<DamageDealerModifier> _modifiers;

    public DamageDealer(Stats stats)
    {
        _stats = stats;
        _modifiers = new Modifiers<DamageDealerModifier>();
    }
    
    public float CalculateDamage(float initialDamage)
    {
        return _modifiers.GetModified(initialDamage, null);
    }

    public void AddModifier(DamageDealerModifier modifier)
    {
        _modifiers.Add(modifier);
    }

    public void RemoveModifier(DamageDealerModifier modifier)
    {
        _modifiers.Remove(modifier);
    }
}