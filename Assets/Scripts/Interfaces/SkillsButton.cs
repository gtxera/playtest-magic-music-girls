using TMPro;
using UnityEngine;

public class SkillsButton : CombatButton
{
    public Skill skill;
    [SerializeField] TextMeshProUGUI attackName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateButtonvalues();
    }


    public void UpdateButtonvalues()
    {
        buttonDescription = skill.BaseDescription;
        attackName.text = skill.BaseName;
    } 

    public void UseSkill()
    {
        battleUIController.IncrementEnergyBarValue(skill.BaseValue);
    }
}
