using System.Collections.Generic;
using UnityEngine;

public class CombatManager : PersistentSingletonBehaviour<CombatManager>
{
    [SerializeField]
    private Transform[] _enemyPositions;

    private List<EnemyUnit> _enemyUnits = new();

    private CombatTurnManager _combatTurnManager;

    protected override void Awake()
    {
        base.Awake();

        _combatTurnManager = new CombatTurnManager();
    }

    public void StartCombat(EnemyUnit[] enemyUnits)
    {
        _enemyUnits.Clear();
        
        for (int i = 0; i < enemyUnits.Length; i++)
        {
            var enemyUnit = Instantiate(enemyUnits[i], _enemyPositions[i].position, Quaternion.identity);
            
            enemyUnit.Health.Died += () => RemoveDeadUnit(enemyUnit);
            enemyUnit.Health.Revived += () => AddRevivedUnit(enemyUnit);

            _enemyUnits.Add(enemyUnit);
        }
    }

    private void RemoveDeadUnit(EnemyUnit unit)
    {
        _enemyUnits.Remove(unit);
    }

    private void AddRevivedUnit(EnemyUnit unit)
    {
        _enemyUnits.Add(unit);
    }
}
