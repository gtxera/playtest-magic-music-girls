using System;
using System.Collections.Generic;
using UnityEngine;

public class Stats
{
    private readonly BaseStats _baseStats;
    private readonly Modifiers<StatModifier> _modifiers;

    public Stats(BaseStats baseStats)
    {
        _baseStats = baseStats;
        _modifiers = new Modifiers<StatModifier>();
    }

    public int Get(Stat stat)
    {
        switch (stat)
        {
            case Stat.Emotion:
                return Emotion;
            case Stat.Virtuosity:
                return Virtuosity;
            case Stat.Endurance:
                return Endurance;
            case Stat.Tempo:
                return Tempo;
            case Stat.Health:
                return Health;
            default:
                throw new ArgumentOutOfRangeException(nameof(stat), stat, null);
        }
    }

    public int Emotion => GetModified(Stat.Emotion, _baseStats.Emotion);
    public int Virtuosity => GetModified(Stat.Virtuosity, _baseStats.Virtuosity);
    public int Endurance => GetModified(Stat.Endurance, _baseStats.Endurance);
    public int Tempo => GetModified(Stat.Tempo, _baseStats.Tempo);
    public int Health => GetModified(Stat.Health, _baseStats.Health);

    public void AddModifier(StatModifier modifier)
    {
        _modifiers.Add(modifier);
    }

    private int GetModified(Stat stat, int initialValue)
    {
        var parametes = new Dictionary<string, object>
        {
            {StatModifier.STAT_PARAMETER_KEY, stat}
        };

        return Mathf.FloorToInt(_modifiers.GetModified(initialValue, parametes));
    }
}
