using UnityEngine;

public class EnemyUnit : Unit
{
    [field: SerializeField]
    public EnemyCharacterData Data { get; private set; }
}
