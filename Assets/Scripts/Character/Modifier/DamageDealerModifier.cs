using System;

[Serializable]
public class DamageDealerModifier : Modifier
{
    protected override bool ShouldModify(ModifyParameters parameters) => true;
}
