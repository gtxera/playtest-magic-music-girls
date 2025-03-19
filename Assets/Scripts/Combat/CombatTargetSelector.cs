using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class CombatTargetSelector : SingletonBehaviour<CombatTargetSelector>
{
    private Skill _skill;
    private Unit _unit;
    private Type _unitType;

    private bool _selecting;

    private HashSet<Unit> _selection = new();
    private int _targetCount;

    public void StartSelection(Skill skill, Unit unit)
    {
        _selecting = true;
        _skill = skill;
        _unit = unit;
        _unitType = skill.TargetType == TargetType.Ally ? typeof(PartyUnit) : typeof(EnemyUnit);
        _targetCount = skill.MaxTargets;
        
        PublishSelectionChangedEvent();
    }

    public bool IsSelectable(Unit unit)
    {
        if (!_selecting)
            return false;

        return unit.GetType() == _unitType;
    }

    public bool IsSelected(Unit unit)
    {
        return _selection.Contains(unit);
    }

    public bool TryAddToSelection(Unit unit)
    {
        if (!IsSelectable(unit))
            return false;
        
        _selection.Add(unit);
        
        PublishSelectionChangedEvent();

        return true;
    }

    public bool TryRemoveFromSelection(Unit unit)
    {
        if (!IsSelected(unit))
            return false;
        
        _selection.Remove(unit);
        
        PublishSelectionChangedEvent();

        return true;
    }

    public void CancelSelection()
    {
        _selecting = false;
        _skill = null;
        _unit = null;
        _selection.Clear();
    }

    public async UniTask ConfirmSelection()
    {
        var action = new CombatAction(_unit, _selection, _skill);
        await CombatManager.Instance.ExecuteAction(action);
    }

    private void PublishSelectionChangedEvent()
    {
        EventBus.Instance.Publish(new SelectionChangedEvent(_selection.Count, GetRemainingTargetsCount(), GetIsAllySelection()));
    }

    private int GetRemainingTargetsCount() => _targetCount - _selection.Count;

    private bool GetIsAllySelection() => _unitType == typeof(PartyUnit);
}
