using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public interface ICombatCommand
{
    UniTask Execute(Unit unit, IEnumerable<Unit> targets);
}
