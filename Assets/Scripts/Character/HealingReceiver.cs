using UnityEngine;

public class HealingReceiver
{
    private readonly Modifiers<HealingReceiverModifier> _modifiers;
    private readonly Stats _stats;

    public HealingReceiver(Stats stats)
    {
        _stats = stats;
        _modifiers = new Modifiers<HealingReceiverModifier>();
    }

    public float CalculateHeal(float initialHeal)
    {
        return _modifiers.GetModified(initialHeal, null);
    }

    public void AddModifier(HealingReceiverModifier modifier)
    {
        _modifiers.Add(modifier);
    }
}
