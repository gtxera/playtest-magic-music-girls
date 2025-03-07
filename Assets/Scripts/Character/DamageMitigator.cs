using UnityEngine;

public class DamageMitigator
{
    private readonly Stats _stats;

    public DamageMitigator(Stats stats)
    {
        _stats = stats;
    }

    public float Mitigate(float damage)
    {
        return damage / Mathf.Pow(2, (float)_stats.Endurance / 100);
    }
}
