using UnityEngine;

public class Stats
{
    private readonly BaseStats _baseStats;
    private readonly Modifiers<StatModifier> _modifiers;

    public Stats(BaseStats baseStats)
    {
        _baseStats = baseStats;
    }

    public int Emotion => _baseStats.Emotion;
    public int Virtuosity => _baseStats.Virtuosity;
    public int Endurance => _baseStats.Endurance;
    public int Tempo => _baseStats.Tempo;
    public int Health => _baseStats.Health;
}
