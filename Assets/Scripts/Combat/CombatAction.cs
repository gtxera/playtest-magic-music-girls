using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;

public class CombatAction
{
    private readonly Unit _unit;
    private readonly IEnumerable<Unit> _targets;
    private readonly ICombatCommand _command;

    public CombatAction(Unit unit, IEnumerable<Unit> targets, ICombatCommand command)
    {
        _unit = unit;
        _targets = targets.ToArray();
        _command = command;
    }

    public async UniTask Do()
    {
        _command.Execute(_unit, _targets);
     
        await CombatAnimationsController.Instance.WaitForAnimations();

        await UniTask.CompletedTask;
    }
}
