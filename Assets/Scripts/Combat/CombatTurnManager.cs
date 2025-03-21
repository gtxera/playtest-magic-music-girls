using System.Collections.Generic;
using System.Linq;

public class CombatTurnManager
{
    private readonly SortedSet<Unit> _turnOrder;
    private SortedSet<Unit> _remainingTurnOrder;

    public int CurrentTurn { get; private set; } = 1;
    public IEnumerable<Unit> TurnOrder => _turnOrder;
    public IEnumerable<Unit> RemainingTurnOrder => _remainingTurnOrder;

    private List<Unit> _revivedUnits = new();
    private HashSet<Unit> _unitsWhoPlayed = new();

    public CombatTurnManager()
    {
        _turnOrder = new SortedSet<Unit>(new UnitTurnOrderComparer());
        _remainingTurnOrder = new SortedSet<Unit>(new UnitTurnOrderComparer());
    }

    public void Start(IEnumerable<Unit> units)
    {
        _turnOrder.Clear();
        CurrentTurn = 1;
        
        foreach (var unit in units)
        {
            _turnOrder.Add(unit);
        }

        CreateRemainingTurnOrder();
        
        EventBus.Instance.Publish(new CombatTurnPassedEvent(_remainingTurnOrder.Min, null, CurrentTurn));
    }

    public void AddToOrder(Unit unit)
    {
        _turnOrder.Add(unit);
        
        _revivedUnits.Add(unit);
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
        
        foreach (var unit in _revivedUnits.Where(unit => !_unitsWhoPlayed.Contains(unit)))
            _remainingTurnOrder.Add(unit);

        if (_remainingTurnOrder.Count == 0)
        {
            CreateRemainingTurnOrder();
            _unitsWhoPlayed.Clear();
            CurrentTurn++;
        }

        var next = _remainingTurnOrder.Min;
        _unitsWhoPlayed.Add(next);
        EventBus.Instance.Publish(new CombatTurnPassedEvent(next, previous, CurrentTurn));
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

        var tempoComparison = x.Stats.Tempo.CompareTo(y.Stats.Tempo) * -1;

        if (tempoComparison != 0)
            return tempoComparison;
        
        return x == y ? 0 : -1;
    }
}