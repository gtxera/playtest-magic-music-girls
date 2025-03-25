using UnityEngine;

public class HealingReceiverModifier : Modifier
{
    protected override Modifier CreateCopy()
    {
        return new HealingReceiverModifier();
    }

    protected override bool ShouldModify(ModifyParameters parameters) => true;
}
