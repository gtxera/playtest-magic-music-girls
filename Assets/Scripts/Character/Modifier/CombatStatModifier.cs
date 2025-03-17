public class CombatStatModifier : StatModifier, ICombatModifier
{
    public CombatStatModifier(string identifier, ModifierType type, Stat stat, float modifyValue) : 
        base(identifier, type, stat, modifyValue) { }
    
    int ICombatModifier.DurationInTurns { get; set; }

    int ICombatModifier.TurnOfCreation { get; set; }

    Unit ICombatModifier.CreatedBy { get; set; }
}
