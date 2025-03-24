using System;
using System.Collections.Generic;
using System.Linq;

public abstract class ConsumableItem : Item, ICombatCommand
{
    public abstract TargetType TargetType { get; }
    public abstract SelectionFlags MandatoryFlags { get; }
    public abstract SelectionFlags ForbiddenFlags { get; }

    public int MaxTargets => 1;

    public void Execute(Unit unit, IEnumerable<Unit> targets)
    {
        if (targets.Count() > 1)
            throw new InvalidOperationException("Item foi selecionado para mais de um alvo");

        if (!Inventory.Instance.TryRemove(this, 1))
            throw new InvalidOperationException("Item foi utilizado sem quantidade minima");

        Execute(unit, targets.First());
    }

    protected abstract void Execute(Unit unit, Unit target);
}
