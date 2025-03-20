using System;
using System.Linq;

public class CombatModifierFactory
{
    private string _identifier;
    private ModifierType _modifierType;
    private int _durationInTurns;

    public CombatModifierFactory WithDuration(int durationInTurns)
    {
        _durationInTurns = durationInTurns;
        return this;
    }

    public CombatModifierFactory WithIdentifier(string identifier)
    {
        _identifier = identifier;
        return this;
    }
    
    public CombatModifierFactory WithModifierType(ModifierType modifierType)
    {
        _modifierType = modifierType;
        return this;
    }

    public CombatModifier Build(Type modifierType, Unit creator, object[] constructorParameters)
    {
        var turnOfCreation = CombatManager.Instance.CurrentTurn;
        
        return (CombatModifier)Activator.CreateInstance(modifierType, GetConstructorParameters(creator, constructorParameters), null);
    }

    private object[] GetConstructorParameters(Unit creator, object[] otherParameters)
    {
        var modifierParameters = new object[]
        {
            _identifier,
            _modifierType
        };
        
        var combatModifierParameters = new object[]
        {
            _durationInTurns,
            CombatManager.Instance.CurrentTurn,
            creator
        };

        return modifierParameters.Concat(otherParameters).Concat(combatModifierParameters).ToArray();
    }
}
