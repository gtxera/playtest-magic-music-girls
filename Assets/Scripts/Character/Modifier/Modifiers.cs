using System.Collections.Generic;
using System.Linq;

public class Modifiers<T> : IEventListener<CombatTurnPassedEvent>, IEventListener<CombatEndedEvent> where T : Modifier
{
    private readonly HashSet<T> _modifiers = new();

    public Modifiers()
    {
        EventBus.Instance.Subscribe(this);
    }

    public void Add(T modifier)
    {
        if (_modifiers.Contains(modifier))
            _modifiers.Remove(modifier);

        _modifiers.Add(modifier);
    }

    public void Remove(T modifier) => _modifiers.Remove(modifier);

    public float GetModified(float initialValue, ModifyParameters parameters)
    {
        if (_modifiers.Count == 0)
            return initialValue;

        return initialValue + _modifiers.Select(m => m.GetValueToAdd(initialValue, parameters))
            .Aggregate((total, next) => total + next);
    }

    public void Handle(CombatTurnPassedEvent @event)
    {
        foreach (var modifier in _modifiers.Where(m => m.IsFinished(@event.Previous, @event.Turn)).ToArray())
            _modifiers.Remove(modifier);
    }

    public void Handle(CombatEndedEvent @event)
    {
        foreach (var modifier in _modifiers.Where(modifier => modifier.Duration > 0).ToArray())
            _modifiers.Remove(modifier);
    }
}
