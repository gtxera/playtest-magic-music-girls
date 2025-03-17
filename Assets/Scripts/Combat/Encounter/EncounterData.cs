using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "EncounterData", menuName = "Scriptable Objects/EncounterData")]
public class EncounterData : ScriptableObject
{
    [SerializeField]
    private EnemyUnit[] _enemies;

    [field: SerializeField]
    public LootTable LootTable {  get; private set; }

    public IEnumerable<EnemyUnit> Enemies => _enemies;
}
