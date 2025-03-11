using TMPro;
using UnityEngine;

public class SkillsButton : CombatButton
{
    public SkillsScript skill;
    [SerializeField] TextMeshProUGUI attackName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateButtonvalues();
    }


    public void UpdateButtonvalues()
    {
        buttonDescription = skill.skillBaseDescription;
        attackName.text = skill.skillBaseName;
    } 

    public void UseSkill()
    {
        battleUIController.IncrementEnergyBarValue(skill.damage);
    }
}
