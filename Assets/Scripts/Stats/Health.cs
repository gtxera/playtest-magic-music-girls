

using System;
using UnityEngine;

public class Health
{
    private readonly Stats _stats;

    private float _health;
    
    public Health(Stats stats)
    {
        _stats = stats;
        _health = MaxHealth;
        _stats.StatModified += OnStatModified;
    }

    public event Action<HealthChangedEventArgs> HealthChanged = delegate { };
    public event Action Died = delegate { };

    public event Action Revived = delegate { };
    
    public float MaxHealth => _stats.Health;

    public bool IsDead => _health <= 0f;
    public float CurrentHealth => _health;

    public void Damage(float damage)
    {
        var previous = _health;
        _health = Mathf.Max(0f, _health - damage);
        
        if (_health == 0f)
            Died.Invoke();
        
        HealthChanged.Invoke(new HealthChangedEventArgs(previous, _health));
    }

    public void Heal(float heal)
    {
        var previous = _health;
        _health = Mathf.Min(_health + heal, MaxHealth);
        
        if (previous == 0f)
            Revived.Invoke();
        
        HealthChanged.Invoke(new HealthChangedEventArgs(previous, _health));
    }

    private void OnStatModified(StatModifiedEventArgs args)
    {
        if (args.ModifiedStat != Stat.Health)
            return;

        if (!(_health > MaxHealth)) 
            return;
        
        var previous = _health;
        _health = MaxHealth;
        HealthChanged.Invoke(new HealthChangedEventArgs(previous, _health));
    }
}

public class HealthChangedEventArgs : EventArgs
{
    public readonly float From;
    public readonly float To;
    public float Change => To - From;

    public HealthChangedEventArgs(float from, float to)
    {
        From = from;
        To = to;
    }
}
