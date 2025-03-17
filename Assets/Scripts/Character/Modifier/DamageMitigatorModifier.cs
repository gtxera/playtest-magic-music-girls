using UnityEngine;

public abstract class DamageMitigatorModifier : Modifier
{
    protected DamageMitigatorModifier(string identifier, ModifierType type) : base(identifier, type)
    {
    }
}
