public abstract class Modifier
{
    public readonly string Identifier;

    public ModifierType Type { get; private set; }

    public Modifier(string identifier, ModifierType type)
    {
        identifier = Identifier;
        Type = type;
    }

    public abstract float Modify(float currentValue, ModifyParameters parameters);

    public override bool Equals(object obj)
    {
        if (obj is not Modifier modifier) 
            return false;

        return Identifier == modifier.Identifier;
    }

    public override int GetHashCode()
    {
        return Identifier.GetHashCode();
    }
}
