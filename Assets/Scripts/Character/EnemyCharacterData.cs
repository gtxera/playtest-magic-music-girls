using UnityEngine;

[CreateAssetMenu(fileName = "EnemyCharacterData", menuName = "Scriptable Objects/EnemyCharacterData")]
public class EnemyCharacterData : CharacterData
{
    [field: SerializeField]
    public int Level { get; private set; }

[field: SerializeField]
    public float ExperienceReward { get; private set; }

    [field: SerializeField]
    public float MoneyReward { get; private set; }
}
