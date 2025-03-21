using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class CombatTargetSelector : SingletonBehaviour<CombatTargetSelector>
{
    private Skill _skill;
    private Unit _unit;
    private Type _unitType;

    private SelectionFlags _mandatoryFlags;
    private SelectionFlags _forbiddenFlags;

    private bool _selecting;

    private HashSet<Unit> _selection = new();
    private int _targetCount;
    private int SelectionCount => _selection.Count;
    private bool IsSelectionFull => _selection.Count == _targetCount;
    
    public bool TryStartSelection(Skill skill, Unit unit)
    {
        _skill = skill;
        _unit = unit;
        _mandatoryFlags = skill.MandatoryFlags;
        _forbiddenFlags = skill.ForbiddenFlags;
        _unitType = GetSelectionType(_unit.GetType(), _skill.TargetType);
        _targetCount = skill.MaxTargets;

        _selecting = true;
        _selecting = GetAvailableSelection().Any();
        
        if (!_selecting)
        {
            CancelSelection();
        }
        
        PublishSelectionChangedEvent(_selecting);

        return _selecting;
    }

    public void AutoSelect(ITargetSelectionStrategy selectionStrategy)
    {
        if (!_selecting)
            Debug.LogError("Tentou selecionar quando não havia seleção ativa");

        var selection = selectionStrategy.GetSelection(GetAvailableSelection(), _targetCount);

        foreach (var unit in selection)
            unit.Select();
    }

    public bool IsAllyOfSelection(Unit unit)
    {
        return unit.GetType() == _unit.GetType();
    }

    public bool IsSelectable(Unit unit)
    {
        if (!_selecting || IsSelected(unit) || IsSelectionFull)
            return false;

        return unit.GetType() == _unitType && unit.SelectionFlags.HasAllFlags(_mandatoryFlags) && !unit.SelectionFlags.HasAnyFlag(_forbiddenFlags);
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
        
        PublishSelectionChangedEvent(true);

        return true;
    }

    public bool TryRemoveFromSelection(Unit unit)
    {
        if (!IsSelected(unit))
            return false;
        
        _selection.Remove(unit);
        
        PublishSelectionChangedEvent(true);

        return true;
    }

    public void CancelSelection()
    {
        _selecting = false;
        _skill = null;
        _unit = null;

        foreach (var unit in _selection)
            unit.Deselect();
        
        _selection.Clear();
    }

    public async UniTask ConfirmSelection()
    {
        await UniTask.SwitchToMainThread();
        foreach (var unit in _selection)
            unit.HideSelection();
        
        var action = new CombatAction(_unit, _selection, _skill);
        CancelSelection();
        await CombatManager.Instance.ExecuteAction(action);
    }

    private IEnumerable<Unit> GetAvailableSelection() => CombatManager.Instance.Units.Where(IsSelectable);

    private void PublishSelectionChangedEvent(bool hasTargets)
    {
        EventBus.Instance.Publish(new SelectionChangedEvent(_selection.Count, GetRemainingTargetsCount(), GetIsAllySelection(), hasTargets));
    }

    private Type GetSelectionType(Type unitType, TargetType targetType)
    {
        if (unitType == typeof(PartyUnit))
            return targetType == TargetType.Ally ? typeof(PartyUnit) : typeof(EnemyUnit);
        else
            return targetType == TargetType.Ally ? typeof(EnemyUnit) : typeof(PartyUnit);
    }

    private int GetRemainingTargetsCount() => _targetCount - _selection.Count;

    private bool GetIsAllySelection() => _unitType == typeof(PartyUnit);
}
