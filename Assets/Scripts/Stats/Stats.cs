using UnityEngine;

public class Stats
{
    private readonly StatsMediator _statsMediator;
    private readonly BaseStats _baseStats;
    
    public Stats(StatsMediator statsMediator, BaseStats baseStats)
    {
        _statsMediator = statsMediator;
        _baseStats = baseStats;
    }

    public int Emotion => _baseStats.Emotion;
    public int Virtuosity => _baseStats.Virtuosity;
    public int Endurance => _baseStats.Endurance;
    public int Tempo => _baseStats.Tempo;
    public int Health => _baseStats.Health;
}
