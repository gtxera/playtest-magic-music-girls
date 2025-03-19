using System;
using UnityEngine;

[Serializable]
public class StatScaling
{
    [field: SerializeField]
    public Stat Stat { get; private set; }
    
    [field: SerializeField]
    public float Scaling { get; private set; }

    public float GetValue(Stats stats)
    {
        return stats.Get(Stat) * Scaling;
    }
}