using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    protected Character Character;

    public Stats Stats => Character.Stats;
    protected Health Health => Character.Health;

    public event Action<HealthChangedEventArgs> HealthChanged = delegate { };
    public event Action Died = delegate { };

    public event Action Revived = delegate { };

    private void RegisterHealthCallbacks()
    {
        Health.HealthChanged += HealthChanged;
        Health.Died += Died;
        Health.Revived += Revived;
    }

    private void DeregisterHealthCallbacks()
    {
        Health.HealthChanged -= HealthChanged;
        Health.Died -= Died;
        Health.Revived -= Revived;
    }

    public void DealDamage(Unit target, float initialDamage, IEnumerable<StatScaling> scalings)
    {
        Character.DealDamage(target.Character, initialDamage, scalings);
    }

    public void Heal(float heal)
    {
        Character.Heal(heal);
    }

    public void AddModifier(Modifier modifier)
    {
        Character.AddModifier(modifier);
    }

    protected virtual void Start()
    {
        RegisterHealthCallbacks();
    }

    protected virtual void OnDestroy()
    {
        DeregisterHealthCallbacks();
    }
}
