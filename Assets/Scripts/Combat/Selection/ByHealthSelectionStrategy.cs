using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "ByHealthSelectionStrategy", menuName = "Scriptable Objects/ByHealthSelectionStrategy")]
public class ByHealthSelectionStrategy : TargetSelectionStrategy
{
    [SerializeField]
    private SelectionStrategyOrder _order;

    [SerializeField]
    private HealthSelectionType _healthSelectionType;
    
    protected override Unit GetNextUnit(IEnumerable<Unit> units)
    {
        var unitWeights = units.ToDictionary(unit => unit, GetHealthSelection);
        var total = unitWeights.Values.Sum();

        var random = Random.Range(0f, total);
        var priority = 0f;
        
        foreach (var kvp in unitWeights)
        {
            priority += kvp.Value;

            if (priority >= random)
                return kvp.Key;
        }

        return null;
    }

    private float GetHealthSelection(Unit unit)
    {
        return _healthSelectionType switch
        {
            HealthSelectionType.Percentage => unit.HealthPercentage,
            HealthSelectionType.Value => unit.CurrentHealth,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private float GetHealthWeight(float value, float total)
    {
        return _order switch
        {
            SelectionStrategyOrder.Lowest => total - value,
            SelectionStrategyOrder.Highest => value,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private enum HealthSelectionType
    {
        Percentage,
        Value
    }
}
