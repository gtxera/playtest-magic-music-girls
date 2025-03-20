using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class CombatManager : SingletonBehaviour<CombatManager>
{
    [SerializeField]
    private CombatPosition[] _enemyPositions;

    [SerializeField]
    private CombatPosition[] _partyPositions;

    private List<EnemyUnit> _enemyUnits = new();
    private List<PartyUnit> _partyUnits = new();

    private CombatTurnManager _combatTurnManager;

    private EncounterData _encounterData;

    public IEnumerable<Unit> Units => _enemyUnits.Select(e => e as Unit).Concat(_partyUnits.Select(p => p as Unit));

    public IEnumerable<Unit> TurnOrder => _combatTurnManager.TurnOrder;
    public IEnumerable<Unit> RemainingTurnOrder => _combatTurnManager.RemainingTurnOrder;

    public int CurrentTurn => _combatTurnManager.CurrentTurn;
    
    protected override void Awake()
    {
        base.Awake();
        _combatTurnManager = new CombatTurnManager();
    }

    public void StartCombat(EncounterData encounterData)
    {
        LevelRoot.Instance.Disable();

        gameObject.SetActive(true);
        
        Input.Instance.SetInputContext(InputContext.Combat);
        
        _encounterData = encounterData;
        
        foreach (var enemyUnitPrefab in encounterData.Enemies)
        {
            var freePosition = _enemyPositions.First(x => !x.IsOccupied);
            var enemyUnit = Instantiate(enemyUnitPrefab, freePosition.transform.position, Quaternion.identity);
            freePosition.Occupy(enemyUnit);
            
            enemyUnit.Died += () => RemoveDeadUnit(enemyUnit);
            enemyUnit.Revived += () => AddRevivedUnit(enemyUnit);
            
            enemyUnit.Initialize();
            
            _enemyUnits.Add(enemyUnit);
        }

        foreach (var partyCharacter in Party.Instance.Characters)
        {
            var freePosition = _partyPositions.First(x => !x.IsOccupied);
            var partyUnit = Instantiate(partyCharacter.PartyUnitPrefab, freePosition.transform.position, Quaternion.identity);
            freePosition.Occupy(partyUnit);

            partyUnit.Died += () => RemoveDeadUnit(partyUnit);
            partyUnit.Revived += () => AddRevivedUnit(partyUnit);
            
            partyUnit.Initialize();
            
            _partyUnits.Add(partyUnit);
        }
        
        _combatTurnManager.Start(Units);
    }

    public async UniTask ExecuteAction(CombatAction action)
    {
        await UniTask.WaitForSeconds(0.5f);
     
        await UniTask.SwitchToMainThread();
        await action.Do();
        await CombatAnimationsController.Instance.WaitForAnimations();
        
        if (CheckCombatEnded(out var playerVictory))
            FinishCombat(playerVictory);

        else
            _combatTurnManager.NextTurn();
    }

    public void SkipTurn()
    {
        _combatTurnManager.NextTurn();
    }

    private void FinishCombat(bool playerVictory)
    {
        EventBus.Instance.Publish(new CombatEndedEvent(playerVictory, GetExperienceReward(), GetMoneyReward(), GetLoot()));
        ResetCombat();
        gameObject.SetActive(false);
        LevelRoot.Instance.Enable();
        Input.Instance.SetInputContext(InputContext.Player);
    }

    private void RemoveDeadUnit(EnemyUnit unit)
    {
        _enemyUnits.Remove(unit);
        _combatTurnManager.RemoveFromOrder(unit);
    }

    private void AddRevivedUnit(EnemyUnit unit)
    {
        _enemyUnits.Add(unit);
        _combatTurnManager.AddToOrder(unit);
    }

    private void RemoveDeadUnit(PartyUnit unit)
    {
        _partyUnits.Remove(unit);
        _combatTurnManager?.RemoveFromOrder(unit);
    }

    private void AddRevivedUnit(PartyUnit unit)
    {
        _partyUnits.Add(unit);
        _combatTurnManager.AddToOrder(unit);
    }

    private bool CheckCombatEnded(out bool playerVictory)
    {
        if (_enemyUnits.Count == 0)
        {
            playerVictory = true;
            return true;
        }

        if (_partyUnits.Count == 0)
        {
            playerVictory = false;
            return true;
        }

        playerVictory = false;
        return false;
    }

    private void ResetCombat()
    {
        foreach (var unit in _enemyUnits)
        {
            Destroy(unit.gameObject);
        }
        _enemyUnits.Clear();
        foreach (var unit in _partyUnits)
        {
            Destroy(unit.gameObject);
        }
        _partyUnits.Clear();

        _combatTurnManager = new CombatTurnManager();

        _encounterData = null;
    }

    private float GetExperienceReward()
    {
        var totalReward = _encounterData.Enemies.Select(unit => unit.Data.ExperienceReward).Aggregate((reward, next) => reward += next);

        var percentage = Random.Range(0.66f, 1f);
        return totalReward * percentage;
    }

    private float GetMoneyReward()
    {
        var totalReward = _encounterData.Enemies.Select(unit => unit.Data.MoneyReward).Aggregate((reward, next) => reward += next);

        var percentage = Random.Range(0.66f, 1f);
        return totalReward * percentage;
    }

    private IEnumerable<Loot> GetLoot()
    {
        return _encounterData.LootTable.GetLoot();
    }
}
