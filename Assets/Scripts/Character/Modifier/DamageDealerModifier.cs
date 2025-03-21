using System;

[Serializable]
public class DamageDealerModifier : Modifier
{
    protected override Modifier CreateCopy()
    {
        return new DamageDealerModifier();
    }

    protected override bool ShouldModify(ModifyParameters parameters) => true;
}
