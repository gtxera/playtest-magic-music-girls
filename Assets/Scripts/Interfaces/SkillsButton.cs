using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillsButton : CombatButton
{
    private SkillCooldown _skillCooldown;

    private Skill _currentSkill;
    
    [SerializeField] TextMeshProUGUI attackName;
    
    private Unit _unit;

    protected override string ButtonDescription => GetDescription();

    public void Initialize(SkillCooldown skillCooldown, Unit unit)
    {
        _skillCooldown = skillCooldown;
        _unit = unit;
        
        Button = GetComponent<Button>();
        battleUIController = FindFirstObjectByType<BattleUIController>();
        battleUIController.EvolvedStateChanged += OnEvolvedStateChanged;

        _currentSkill = _skillCooldown.Skill;
        
        SetSkillView();
        Button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        CombatTargetSelector.Instance.TryStartSelection(_currentSkill, _unit);
        battleUIController.ChangeOptionsPanel(CombatPanel.Selection);
    }

    private string GetDescription()
    {
        var builder = new StringBuilder(_currentSkill.GetDescription());
        builder.AppendLine();

        if (_currentSkill.SkillType == SkillType.Normal)
            builder.Append(_skillCooldown.GetUseIntervalText());
        
        return builder.ToString();
    }

    private void OnEvolvedStateChanged(bool isEvolved)
    {
        _currentSkill = isEvolved ? _skillCooldown.Skill.EvolvedSkill : _skillCooldown.Skill;
        SetSkillView();
    }

    private void SetSkillView()
    {
        attackName.SetText(_currentSkill.BaseName);
        Button.interactable = _currentSkill.SkillType == SkillType.Evolved ? true : _skillCooldown.Available;
    }
}
