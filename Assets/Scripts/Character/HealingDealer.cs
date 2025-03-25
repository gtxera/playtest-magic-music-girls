using UnityEngine;

public class HealingDealer
{
    private readonly Modifiers<HealingDealerModifier> _modifiers;
    private readonly Stats _stats;

    public HealingDealer(Stats stats)
    {
        _stats = stats;
        _modifiers = new Modifiers<HealingDealerModifier>();
    }

    public float CalculateHeal(float initialHeal)
    {
        return _modifiers.GetModified(initialHeal, null);
    }

    public void AddModifier(HealingDealerModifier modifier)
    {
        _modifiers.Add(modifier);
    }
}
