using System;

[Serializable]
public class DamageMitigatorModifier : Modifier
{
    protected override Modifier CreateCopy()
    {
        return new DamageMitigatorModifier();
    }

    protected override bool ShouldModify(ModifyParameters parameters) => true;
}
