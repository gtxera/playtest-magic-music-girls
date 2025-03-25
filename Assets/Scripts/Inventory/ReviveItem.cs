using UnityEngine;

[CreateAssetMenu(fileName = "ReviveItem", menuName = "Scriptable Objects/Items/ReviveItem")]
public class ReviveItem : ConsumableItem
{
    [SerializeField]
    private float _healthOnRevive;

    public override TargetType TargetType => TargetType.Ally;

    public override SelectionFlags MandatoryFlags => 0;

    public override SelectionFlags ForbiddenFlags => SelectionFlags.Alive;

    public override void Use(Character character)
    {
        character.Heal(_healthOnRevive);
    }

    protected override void Execute(Unit unit, Unit target)
    {
        unit.Heal(target, _healthOnRevive);
    }
}
