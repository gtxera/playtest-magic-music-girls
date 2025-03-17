using UnityEngine;

[CreateAssetMenu(fileName = "EnemyCharacterData", menuName = "Scriptable Objects/EnemyCharacterData")]
public class EnemyCharacterData : ScriptableObject
{
    [field: SerializeField]
    public CharacterData CharacterData { get; private set; }

    [field: SerializeField]
    public float ExperienceReward { get; private set; }

    [field: SerializeField]
    public float MoneyReward { get; private set; }
}
