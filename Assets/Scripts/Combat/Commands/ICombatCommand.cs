using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public interface ICombatCommand
{
    void Execute(Unit unit, IEnumerable<Unit> targets);
    TargetType TargetType { get; }
    SelectionFlags MandatoryFlags { get; }
    SelectionFlags ForbiddenFlags { get; }
    int MaxTargets { get; }
    string Name { get; }
}
