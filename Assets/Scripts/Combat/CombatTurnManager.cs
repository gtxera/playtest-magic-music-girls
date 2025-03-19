using System.Collections.Generic;
using UnityEngine;

public class CombatTurnManager
{
    private readonly SortedSet<Unit> _turnOrder;
    private SortedSet<Unit> _remainingTurnOrder;

    public int CurrentTurn { get; private set; } = 1;
    public IEnumerable<Unit> TurnOrder => _turnOrder;
    public IEnumerable<Unit> RemainingTurnOrder => _remainingTurnOrder;

    public CombatTurnManager()
    {
        _turnOrder = new SortedSet<Unit>(new UnitTurnOrderComparer());
        _remainingTurnOrder = new SortedSet<Unit>(new UnitTurnOrderComparer());
    }

    public void PopulateUnits(IEnumerable<Unit> units)
    {
        _turnOrder.Clear();
        CurrentTurn = 1;
        
        foreach (var unit in units)
        {
            _turnOrder.Add(unit);
        }

        CreateRemainingTurnOrder();
    }

    public void AddToOrder(Unit unit)
    {
        _turnOrder.Add(unit);
    }

    public void RemoveFromOrder(Unit unit)
    {
        _turnOrder.Remove(unit);

        if (_remainingTurnOrder.Contains(unit))
        {
            _remainingTurnOrder.Remove(unit);
        }
    }

    public void NextTurn()
    {
        var previous = _remainingTurnOrder.Min;
        _remainingTurnOrder.Remove(previous);

        if (_remainingTurnOrder.Count == 0)
        {
            CreateRemainingTurnOrder();
            CurrentTurn++;
        }

        var next = _remainingTurnOrder.Min;
        EventBus.Instance.Publish(new CombatTurnPassedEvent(next, CurrentTurn));
    }

    private void CreateRemainingTurnOrder() => _remainingTurnOrder = new SortedSet<Unit>(_turnOrder, new UnitTurnOrderComparer());
}

public class UnitTurnOrderComparer : IComparer<Unit>
{
    public int Compare(Unit x, Unit y)
    {
        if (x == y)
            return 0;
        if (x == null)
            return -1;
        if (y == null)
            return 1;

        return x.Stats.Tempo.CompareTo(y.Stats.Tempo);
    }
}