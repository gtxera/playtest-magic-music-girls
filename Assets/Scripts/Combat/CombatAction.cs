using Cysharp.Threading.Tasks;
using UnityEngine;

public class CombatAction
{
    private readonly ICombatCommand _command;

    public CombatAction(ICombatCommand command)
    {
        _command = command;
    }

    public UniTask ExecuteAsync()
    {
        _command.Execute();

        return UniTask.CompletedTask;
    }
}
