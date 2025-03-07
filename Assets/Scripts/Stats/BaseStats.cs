using System;
using UnityEngine;

[Serializable]
public class BaseStats
{
    [field: SerializeField]
    public int Emotion { get; private set; }
    
    [field: SerializeField]
    public int Virtuosity { get; private set; }
    
    [field: SerializeField]
    public int Endurance { get; private set; }
    
    [field: SerializeField]
    public int Tempo { get; private set; }
    
    [field: SerializeField]
    public int Health { get; private set; }

    public static BaseStats operator +(BaseStats lhr, BaseStats rhs)
    {
        var sum = new BaseStats
        {
            Emotion = lhr.Emotion + rhs.Emotion,
            Virtuosity = lhr.Virtuosity + rhs.Virtuosity,
            Endurance = lhr.Endurance + rhs.Endurance,
            Tempo = lhr.Tempo + rhs.Tempo,
            Health = lhr.Health + rhs.Health
        };

        return sum;
    }

    public static BaseStats operator *(BaseStats stats, int multiplier)
    {
        var product = new BaseStats
        {
            Emotion = stats.Emotion * multiplier,
            Virtuosity = stats.Virtuosity * multiplier,
            Endurance = stats.Endurance * multiplier,
            Tempo = stats.Tempo * multiplier,
            Health = stats.Health * multiplier
        };

        return product;
    }
}
