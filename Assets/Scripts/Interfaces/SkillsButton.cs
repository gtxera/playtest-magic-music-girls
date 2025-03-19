using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillsButton : CombatButton
{
    [SerializeField]
    private Skill _skill;
    [SerializeField] TextMeshProUGUI attackName;

    private Unit _unit;
    
    public void Initialize(Skill skill, Unit unit)
    {
        _skill = skill;
        _unit = unit;
        
        Button = GetComponent<Button>();
        battleUIController = FindFirstObjectByType<BattleUIController>();
        
        buttonDescription = _skill.BaseDescription;
        attackName.text = _skill.BaseName;
        Button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        CombatTargetSelector.Instance.StartSelection(_skill, _unit);
        battleUIController.ChangeOptionsPanel(CombatPanel.Selection);
    }
}
