using System;

[Serializable]
public class DamageMitigatorModifier : Modifier
{ 
    protected override bool ShouldModify(ModifyParameters parameters) => true;
}
