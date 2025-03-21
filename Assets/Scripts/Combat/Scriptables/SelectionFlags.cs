using System;

[Flags]
public enum SelectionFlags
{
    Alive = 1 << 1,
    FullHealth = 1 << 2
}

public static class SelectionFlagsExtensions
{
    public static bool HasAnyFlag(this SelectionFlags flags, SelectionFlags other)
    {
        return (flags & other) != 0;
    }

    public static bool HasAllFlags(this SelectionFlags flags, SelectionFlags other)
    {
        return (flags & other) == other;
    }
}