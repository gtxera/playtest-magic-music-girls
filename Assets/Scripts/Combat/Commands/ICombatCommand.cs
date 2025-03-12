using System.Collections.Generic;

public interface ICombatCommand
{
    void Execute(Unit unit, IEnumerable<Unit> targets);
}
