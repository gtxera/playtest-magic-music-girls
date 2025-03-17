using System;
using System.Linq;

public class CombatModifierFactory<T> where T : ICombatModifier
{
    private int _durationInTurns;
    private int _turnOfCreation;
    private Unit _createdBy;

    public CombatModifierFactory<T> WithDuration(int durationInTurns)
    {
        _durationInTurns = durationInTurns;

        return this;
    }

    public T Build(Unit creator, params object[] constructorParameters)
    {
        return (T)Activator.CreateInstance(typeof(T), constructorParameters.Concat(new object[]{_durationInTurns, _turnOfCreation, creator}), null);
    }
}
