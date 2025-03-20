using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ByHealthSelectionStrategy", menuName = "Scriptable Objects/ByHealthSelectionStrategy")]
public class ByHealthSelectionStrategy : TargetSelectionStrategy
{
    [SerializeField]
    private SelectionStrategyOrder _order;

    [SerializeField]
    private HealthSelectionType _healthSelectionType;
    
    protected override Unit GetNextUnit(IEnumerable<Unit> units)
    {
        return _order switch
        {
            SelectionStrategyOrder.Lowest => units.OrderBy(GetHealthOrder).First(),
            SelectionStrategyOrder.Highest => units.OrderByDescending(GetHealthOrder).First(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private float GetHealthOrder(Unit unit)
    {
        return _healthSelectionType switch
        {
            HealthSelectionType.Percentage => unit.HealthPercentage,
            HealthSelectionType.Value => unit.CurrentHealth,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private enum HealthSelectionType
    {
        Percentage,
        Value
    }
}
