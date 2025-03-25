using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class CombatManager : SingletonBehaviour<CombatManager>
{
    [SerializeField]
    private CombatPosition[] _enemyPositions;

    [SerializeField]
    private CombatPosition[] _partyPositions;

    private List<EnemyUnit> _enemyUnits = new();
    private List<PartyUnit> _partyUnits = new();

    private CombatTurnManager _combatTurnManager = new();

    private EncounterData _encounterData;

    public CombatComboManager ComboManager { get; private set; } = new();

    public IEnumerable<Unit> Units => _enemyUnits.Select(e => e as Unit).Concat(_partyUnits.Select(p => p as Unit));

    public IEnumerable<Unit> TurnOrder => _combatTurnManager.TurnOrder;
    public IEnumerable<Unit> RemainingTurnOrder => _combatTurnManager.RemainingTurnOrder;

    public int CurrentTurn => _combatTurnManager.CurrentTurn;

    private Component _encounterStarter;

    public void StartCombat(EncounterData encounterData, Component encounterStarter)
    {
        LevelRoot.Instance.Disable();

        _combatTurnManager = new CombatTurnManager();
        
        gameObject.SetActive(true);
        
        Input.Instance.SetInputContext(InputContext.Combat);
        
        _encounterData = encounterData;
        _encounterStarter = encounterStarter;

        ComboManager = new CombatComboManager();

        foreach (var enemyUnitPrefab in encounterData.Enemies)
        {
            var freePosition = _enemyPositions.First(x => !x.IsOccupied);
            var enemyUnit = Instantiate(enemyUnitPrefab, freePosition.transform.position, Quaternion.identity);
            freePosition.Occupy(enemyUnit);
            
            enemyUnit.Died += () => _combatTurnManager.RemoveFromOrder(enemyUnit);
            enemyUnit.Revived += () => _combatTurnManager.AddToOrder(enemyUnit);
            
            enemyUnit.Initialize();
            
            _enemyUnits.Add(enemyUnit);
        }

        foreach (var partyCharacter in Party.Instance.Characters)
        {
            var freePosition = _partyPositions.First(x => !x.IsOccupied);
            var partyUnit = Instantiate(partyCharacter.PartyUnitPrefab, freePosition.transform.position, Quaternion.identity);
            freePosition.Occupy(partyUnit);
            
            partyUnit.Died += () => _combatTurnManager.RemoveFromOrder(partyUnit);
            partyUnit.Revived += () => _combatTurnManager.AddToOrder(partyUnit);
            
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

        if (CheckCombatEnded(out var playerVictory))
        {
            await UniTask.SwitchToMainThread();
            NotifyCombatEnded(playerVictory);
        }

        await HandleCombo(action.Unit);

        if (CheckCombatEnded(out playerVictory))
        {
            await UniTask.SwitchToMainThread();
            NotifyCombatEnded(playerVictory);
        }

        else
            _combatTurnManager.NextTurn();
    }

    private async UniTask HandleCombo(Unit unit)
    {
        if (unit.GetType() != typeof(PartyUnit))
            return;

        if (!ComboManager.HasComboReady(out var combo))
            return;

        if (!CombatTargetSelector.Instance.TryStartSelection(combo.Skill, unit))
            return;

        CombatTargetSelector.Instance.AutoSelect(combo.Skill.TargetSelectionStrategy);
        await CombatTargetSelector.Instance.ConfirmSelection();
    }

    public void SkipTurn()
    {
        _combatTurnManager.NextTurn();
    }

    public void FinishCombat()
    {
        ResetCombat();
        LevelRoot.Instance.Enable();
        Input.Instance.SetInputContext(InputContext.Player);
        gameObject.SetActive(false);
    }

    private void NotifyCombatEnded(bool playerVictory)
    {
        EventBus.Instance.Publish(new CombatEndedEvent(playerVictory, GetExperienceReward(), GetMoneyReward(), _encounterStarter, GetLoot()));
    }

    private bool CheckCombatEnded(out bool playerVictory)
    {
        if (_enemyUnits.All(e => e.IsDead))
        {
            playerVictory = true;
            return true;
        }

        if (_partyUnits.All(p => p.IsDead))
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
        
        _encounterData = null;
        _encounterStarter = null;
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
