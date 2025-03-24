using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : CombatButton
{
    private ConsumableItem _item;
    private Unit _unit;

    [SerializeField]
    private TextMeshProUGUI _text;

    protected override string ButtonDescription => _item.Description;

    public void Initialize(ConsumableItem item, Unit unit)
    {
        _item = item;
        _unit = unit;

        Button = GetComponent<Button>();
        battleUIController = FindFirstObjectByType<BattleUIController>();

        _text.SetText(GetButtonText());
        Button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        CombatTargetSelector.Instance.TryStartSelection(_item, _unit);
        battleUIController.ChangeOptionsPanel(CombatPanel.Selection);
    }

    private string GetButtonText()
    {
        return $"{_item.Name} x{Inventory.Instance.GetItemCount(_item)}";
    }
}
