using UnityEngine;

public class HealingDealerModifier : Modifier
{
    protected override Modifier CreateCopy()
    {
        return new HealingDealerModifier();
    }

    protected override bool ShouldModify(ModifyParameters parameters) => true;
}
