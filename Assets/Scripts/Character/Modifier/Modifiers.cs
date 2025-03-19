using System.Collections.Generic;

public class Modifiers<T> : IEventListener<CombatTurnPassedEvent>, IEventListener<CombatEndedEvent> where T : Modifier
{
    private readonly SortedSet<T> _modifiers = new();

    public void Add(T modifier)
    {
        if (_modifiers.Contains(modifier))
            _modifiers.Remove(modifier);

        _modifiers.Add(modifier);
    }

    public void Remove(T modifier) => _modifiers.Remove(modifier);

    public float GetModified(float initialValue, ModifyParameters parameters)
    {
        var finalValue = initialValue;

        foreach (var modifier in _modifiers)
        {
            finalValue = modifier.Modify(finalValue, parameters);
        }

        return finalValue;
    }

    public void Handle(CombatTurnPassedEvent @event)
    {
        foreach (var modifier in _modifiers)
        {
            if (modifier is not ICombatModifier combatModifier)
                continue;

            if (combatModifier.Finished(@event.Unit, @event.Turn))
                _modifiers.Remove(modifier);
        }

    }

    public void Handle(CombatEndedEvent @event)
    {
        foreach (var modifier in _modifiers)
            if (modifier is ICombatModifier)
                _modifiers.Remove(modifier);
    }
}
