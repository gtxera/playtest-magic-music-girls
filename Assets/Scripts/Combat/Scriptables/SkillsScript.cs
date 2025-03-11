using UnityEngine;

[CreateAssetMenu(fileName = "SkillsScript", menuName = "Scriptable Objects/SkillsScript")]
public class SkillsScript : ScriptableObject
{
    [Header("Skill Base Stats")]
    public string skillBaseName;
    public string skillBaseDescription;
    public float damage;
    public float heal;

    [Header("Skill Evolved Stats")]
    public string skillEvolvedName;
    public string skillEvolvedDescription;
    public float evolvedDamage;
    public float evolvedHeal;
}
