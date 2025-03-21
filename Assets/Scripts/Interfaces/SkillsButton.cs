using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillsButton : CombatButton
{
    private SkillCooldown _skillCooldown;
    
    [SerializeField] TextMeshProUGUI attackName;
    
    private Unit _unit;

    protected override string ButtonDescription => GetDescription();

    public void Initialize(SkillCooldown skillCooldown, Unit unit)
    {
        _skillCooldown = skillCooldown;
        _unit = unit;
        
        Button = GetComponent<Button>();
        battleUIController = FindFirstObjectByType<BattleUIController>();
        
        attackName.text = _skillCooldown.Skill.BaseName;
        Button.onClick.AddListener(OnClick);
        Button.interactable = _skillCooldown.Available;
    }

    private void OnClick()
    {
        CombatTargetSelector.Instance.TryStartSelection(_skillCooldown.Skill, _unit);
        battleUIController.ChangeOptionsPanel(CombatPanel.Selection);
    }

    private string GetDescription()
    {
        var builder = new StringBuilder(_skillCooldown.Skill.GetDescription());
        builder.AppendLine();

        builder.Append(_skillCooldown.GetUseIntervalText());
        
        return builder.ToString();
    }
}
