using UnityEngine;

[CreateAssetMenu(fileName = "HealingItem", menuName = "Scriptable Objects/Items/HealingItem")]
public class HealingItem : ConsumableItem
{
    [SerializeField]
    private float _healingAmount;

    public override TargetType TargetType => TargetType.Ally;

    public override SelectionFlags MandatoryFlags => SelectionFlags.Alive;

    public override SelectionFlags ForbiddenFlags => SelectionFlags.FullHealth;

    public override void Use(Character character)
    {
        character.Heal(_healingAmount);
    }

    protected override void Execute(Unit unit, Unit target)
    {
        target.Heal(_healingAmount);
    }
}
