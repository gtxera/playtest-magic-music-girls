using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class TargetSelectionStrategy : ScriptableObject, ITargetSelectionStrategy
{
    public IEnumerable<Unit> GetSelection(IEnumerable<Unit> units, int targetCount)
    {
        var unitList = units.ToList();
        var selection = new List<Unit>();
        
        do
        {
            var next = GetNextUnit(unitList);
            unitList.Remove(next);
            selection.Add(next);
        } while (unitList.Count > 0 || selection.Count < targetCount);

        return selection;
    }

    protected abstract Unit GetNextUnit(IEnumerable<Unit> units);
}

public enum SelectionStrategyOrder
{
    Lowest,
    Highest
}