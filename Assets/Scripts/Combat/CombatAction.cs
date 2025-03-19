using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class CombatAction
{
    private readonly Unit _unit;
    private readonly IEnumerable<Unit> _targets;
    private readonly ICombatCommand _command;

    public async UniTask Do()
    {
        _command.Execute(_unit, _targets);

        await UniTask.CompletedTask;
    }
}
