using UnityEngine;

public abstract class DamageDealerModifier : Modifier
{
    protected DamageDealerModifier(string identifier, ModifierType type) : base(identifier, type)
    {
    }
}
