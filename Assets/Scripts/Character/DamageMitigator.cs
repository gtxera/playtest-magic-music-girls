using UnityEngine;

public class DamageMitigator
{
    private readonly Stats _stats;
    private readonly Modifiers<DamageMitigatorModifier> _modifiers;

    public DamageMitigator(Stats stats)
    {
        _stats = stats;
        _modifiers = new Modifiers<DamageMitigatorModifier>();
    }

    public float Mitigate(float damage)
    {
        var baseDamage = damage / Mathf.Pow(2, (float)_stats.Endurance / 100);

        return _modifiers.GetModified(baseDamage, null);
    }

    public void AddModifier(DamageMitigatorModifier modifier)
    {
        _modifiers.Add(modifier);
    }
    
    public void RemoveModifier(DamageMitigatorModifier modifier)
    {
        _modifiers.Remove(modifier);
    }
}
